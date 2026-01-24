using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Nerosoft.Euonia.Bus;
using Nerosoft.Euonia.Security;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Repository.Models;
using Nerosoft.Starfish.Repository.Requests;
using Nerosoft.Starfish.Repository.Specifications;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Handlers;

internal class AuthlogQueryHandler : IHandler<AuthlogSearchQuery, IList<AuthlogListModel>>, IHandler<AuthlogCountQuery, int>
{
	private readonly IdentityDataContext _context;
	private readonly UserPrincipal _user;

	public AuthlogQueryHandler(IdentityDataContext context, UserPrincipal user)
	{
		_context = context;
		_user = user;
	}

	public async Task<IList<AuthlogListModel>> HandleAsync(AuthlogSearchQuery message, MessageContext context, CancellationToken cancellationToken = default)
	{
		var predicate = ApplyCriteria(message.Criteria);
		var query = _context.Set<Authlog>()
			.AsNoTracking()
			.Where(predicate)
			.OrderByDescending(x => x.Timestamp)
			.Skip(message.Skip)
			.Take(message.Size)
			.Select(x => new AuthlogListModel
			{
				Username = x.Username,
				Source = x.Source,
				GrantType = x.GrantType,
				Timestamp = x.Timestamp,
				Success = x.Success,
				AppName = x.AppName,
				AppVersion = x.AppVersion,
				IpAddress = x.IpAddress,
				OsPlatform = x.OsPlatform,
				Remark = x.Remark,
				RequestId = x.RequestId
			});
		var result = await query.ToListAsync(cancellationToken);
		return result;
	}

	public Task<int> HandleAsync(AuthlogCountQuery message, MessageContext context, CancellationToken cancellationToken = default)
	{
		var predicate = ApplyCriteria(message.Criteria);
		var query = _context.Set<Authlog>().AsNoTracking().Where(predicate);

		return query.CountAsync(cancellationToken);
	}

	private Expression<Func<Authlog, bool>> ApplyCriteria(AuthlogCriteriaModel criteria)
	{
		if (!_user.IsInRoles(RoleName.Admin))
		{
			criteria.Username = _user.Username;
		}

		var specification = AuthlogSpecification.All;

		if (!string.IsNullOrWhiteSpace(criteria.UserId))
		{
			specification &= AuthlogSpecification.UserIdEquals(criteria.UserId);
		}

		if (!string.IsNullOrWhiteSpace(criteria.Username))
		{
			specification &= AuthlogSpecification.UsernameEquals(criteria.Username);
		}

		if (criteria.Success is not null)
		{
			specification &= AuthlogSpecification.ResultEquals(criteria.Success.Value);
		}

		if (criteria.From is not null)
		{
			specification &= AuthlogSpecification.After(criteria.From.Value);
		}

		if (criteria.To is not null)
		{
			specification &= AuthlogSpecification.Before(criteria.To.Value);
		}

		if (!string.IsNullOrWhiteSpace(criteria.Source))
		{
			specification &= AuthlogSpecification.SourceEquals(criteria.Source);
		}

		return specification.Satisfy();
	}
}
