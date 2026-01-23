using Microsoft.EntityFrameworkCore;
using Nerosoft.Euonia.Bus;
using Nerosoft.Euonia.Linq;
using Nerosoft.Euonia.Mapping;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Repository.Models;
using Nerosoft.Starfish.Repository.Requests;
using Nerosoft.Starfish.Repository.Specifications;
using Nerosoft.Starfish.Shared;
using Nerosoft.Starfish.Toolkit;

namespace Nerosoft.Starfish.Repository.Handlers;

internal class UserQueryHandler : IHandler<UserPasswordVerifyRequest, bool>,
                                  IHandler<UserDetailQuery, UserDetailModel>,
                                  IHandler<UserSearchQuery, List<UserListModel>>,
                                  IHandler<UserCountQuery, int>,
                                  IHandler<UserAuthInfoQuery, UserAuthInfoModel>
{
	private readonly IdentityDataContext _context;

	public UserQueryHandler(IdentityDataContext context)
	{
		_context = context;
	}

	public async Task<bool> HandleAsync(UserPasswordVerifyRequest message, MessageContext context, CancellationToken cancellationToken = default)
	{
		var query = from user in _context.Set<User>()
		            where user.Id == message.Id
		            select new { user.PasswordHash, user.PasswordSalt };
		var password = await query.FirstOrDefaultAsync(cancellationToken);

		var secretHash = Cryptography.DES.Encrypt(message.Password, Encoding.UTF8.GetBytes(password.PasswordSalt));

		return string.Equals(password.PasswordHash, secretHash, StringComparison.Ordinal);
	}

	public async Task<UserDetailModel> HandleAsync(UserDetailQuery message, MessageContext context, CancellationToken cancellationToken = default)
	{
		var specification = UserSpecification.IdEquals(message.Id);

		var predicate = specification.Satisfy();

		var query = _context.Set<User>()
		                    .Include(t => t.Roles)
		                    .Where(predicate);
		var user = await query.FirstOrDefaultAsync(cancellationToken);

		if (user == null)
		{
			return null;
		}

		var model = TypeAdapter.ProjectedAs<UserDetailModel>(user);

		var authlog = await _context.Set<Authlog>()
		                            .OrderByDescending(x => x.Id)
		                            .Where(x => x.Username == user.Username && x.Success)
		                            .FirstOrDefaultAsync(cancellationToken);
		if (authlog != null)
		{
			model.LastLoginAt = authlog.Timestamp;
		}

		return model;
	}

	public async Task<List<UserListModel>> HandleAsync(UserSearchQuery message, MessageContext context, CancellationToken cancellationToken = default)
	{
		var specification = UserSpecification.True()
		                                     .AndIf(!string.IsNullOrWhiteSpace(message.Keyword), () => UserSpecification.ContainsKeyword(message.Keyword))
		                                     .AndIf(message.Locked.HasValue, () => UserSpecification.IsLocked(message.Locked!.Value));

		var predicate = specification.Satisfy();

		var entities = await _context.Set<User>()
		                             .AsNoTracking()
		                             .Where(predicate)
		                             .OrderBy(x => x.CreatedAt)
		                             .Skip(message.Skip)
		                             .Take(message.Size)
		                             .ToListAsync(cancellationToken);

		return TypeAdapter.ProjectedAs<List<UserListModel>>(entities);
	}

	public Task<int> HandleAsync(UserCountQuery message, MessageContext context, CancellationToken cancellationToken = default)
	{
		var specification = UserSpecification.True()
		                                     .AndIf(!string.IsNullOrWhiteSpace(message.Keyword), () => UserSpecification.ContainsKeyword(message.Keyword))
		                                     .AndIf(message.Locked.HasValue, () => UserSpecification.IsLocked(message.Locked!.Value));

		var predicate = specification.Satisfy();

		return _context.Set<User>()
		               .AsNoTracking()
		               .Where(predicate)
		               .CountAsync(cancellationToken);
	}

	public async Task<UserAuthInfoModel> HandleAsync(UserAuthInfoQuery message, MessageContext context, CancellationToken cancellationToken = default)
	{
		var specification = message.Provider switch
		{
			AuthProvider.Identifier => UserSpecification.IdEquals(message.Name),
			AuthProvider.Email => UserSpecification.EmailEquals(message.Name),
			AuthProvider.Phone => UserSpecification.PhoneEquals(message.Name),
			AuthProvider.Username => UserSpecification.UsernameEquals(message.Name),
			_ => UserSpecification.AuthorityEquals(message.Provider, message.Name)
		};

		var predicate = specification.Satisfy();

		var entity = await _context.Set<User>()
		                           .AsNoTracking()
		                           .Include(u => u.Roles)
		                           .Include(u => u.Authorities)
		                           .FirstOrDefaultAsync(predicate, cancellationToken);

		return TypeAdapter.ProjectedAs<UserAuthInfoModel>(entity);

		// var query = from user in _context.Set<User>().Include(u => u.Authorities)
		//             where predicate.Compile()(user)
		//             select new UserAuthModel
		//             {
		// 	            Id = user.Id,
		// 	            Username = user.Username,
		// 	            Email = user.Email,
		// 	            Phone = user.Phone,
		// 	            Nickname = user.Nickname,
		// 	            Roles = user.Roles.Select(r => r.Name).ToHashSet()
		//             };
	}
}