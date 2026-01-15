using Microsoft.EntityFrameworkCore;
using Nerosoft.Euonia.Bus;
using Nerosoft.Euonia.Mapping;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Repository.Models;
using Nerosoft.Starfish.Repository.Requests;
using Nerosoft.Starfish.Repository.Specifications;
using Nerosoft.Starfish.Toolkit;

namespace Nerosoft.Starfish.Repository.Handlers;

internal class UserRequestHandler : IHandler<UserPasswordVerifyRequest, bool>,
                                    IHandler<UserDetailQueryRequest, UserDetailQueryModel>
{
	private readonly IdentityDataContext _context;

	public UserRequestHandler(IdentityDataContext context)
	{
		_context = context;
	}

	public async Task<bool> HandleAsync(UserPasswordVerifyRequest message, MessageContext context, CancellationToken cancellationToken = new CancellationToken())
	{
		var query = from user in _context.Set<User>()
		            where user.Id == message.Id
		            select new { user.PasswordHash, user.PasswordSalt };
		var password = await query.FirstOrDefaultAsync(cancellationToken);

		var secretHash = Cryptography.DES.Encrypt(message.Password, Encoding.UTF8.GetBytes(password.PasswordSalt));

		return string.Equals(password.PasswordHash, secretHash, StringComparison.Ordinal);
	}

	public async Task<UserDetailQueryModel> HandleAsync(UserDetailQueryRequest message, MessageContext context, CancellationToken cancellationToken = new CancellationToken())
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

		var model = TypeAdapter.ProjectedAs<UserDetailQueryModel>(user);

		var authlog = await _context.Set<Authlog>()
		                            .OrderByDescending(x => x.Timestamp)
		                            .Where(x => x.Username == user.Username && x.Success)
		                            .FirstOrDefaultAsync(cancellationToken);
		if (authlog != null)
		{
			model.LastLoginAt = authlog.Timestamp;
		}

		return model;
	}
}