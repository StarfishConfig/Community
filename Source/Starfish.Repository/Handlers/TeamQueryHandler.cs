using Microsoft.EntityFrameworkCore;
using Nerosoft.Euonia.Bus;
using Nerosoft.Euonia.Linq;
using Nerosoft.Euonia.Security;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Repository.Models;
using Nerosoft.Starfish.Repository.Specifications;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Handlers;

internal class TeamQueryHandler : IHandler<TeamSearchQuery, IList<TeamListModel>>,
                                  IHandler<TeamCountQuery, int>,
                                  IHandler<TeamDetailQuery, TeamDetailModel>,
                                  IHandler<TeamMemberListQuery, IList<TeamMemberModel>>,
                                  IHandler<TeamMemberCountQuery, int>
{
	private readonly IdentityDataContext _context;
	private readonly UserPrincipal _user;

	public TeamQueryHandler(IdentityDataContext context, UserPrincipal user)
	{
		_context = context;
		_user = user;
	}

	public async Task<IList<TeamListModel>> HandleAsync(TeamSearchQuery message, MessageContext context, CancellationToken cancellationToken = default)
	{
		var teams = _context.Set<Team>().AsNoTracking();
		var users = _context.Set<User>().AsNoTracking();

		var query = from team in teams
		            join user in users on team.OwnerId equals user.Id
		            orderby team.Id descending
		            select new TeamListModel
		            {
			            Id = team.Id,
			            Name = team.Name,
			            Description = team.Description,
			            MembersCount = team.MembersCount,
			            CreatedAt = team.CreatedAt,
			            UpdatedAt = team.UpdatedAt,
			            OwnerId = team.OwnerId,
			            OwnerUsername = user.Username,
			            OwnerNickname = user.Nickname,
		            };

		var predicate = PredicateBuilder.True<TeamListModel>();

		if (!_user.IsInRoles("sa"))
		{
			var memberTeamIds = _context.Set<TeamMember>()
			                            .AsNoTracking()
			                            .Where(m => m.UserId == _user.UserId)
			                            .Select(m => m.TeamId);
			predicate = predicate.And(t => memberTeamIds.Contains(t.Id));
		}

		predicate = message.Owned switch
		{
			true => predicate.And(t => t.OwnerId == _user.UserId),
			false => predicate.And(t => t.OwnerId != _user.UserId),
			_ => predicate
		};

		if (!string.IsNullOrWhiteSpace(message.Keyword))
		{
			var keyword = message.Keyword.Normalize(TextCaseType.Lower);
			predicate = predicate.And(t => t.Name.ToLower().Contains(keyword) || t.Description.ToLower().Contains(keyword) || t.OwnerUsername.ToLower().Contains(keyword) || t.OwnerNickname.ToLower().Contains(keyword));
		}

		{
		}

		return await query.Where(predicate)
		                  .OrderByDescending(t => t.Id)
		                  .Skip(message.Skip).Take(message.Size)
		                  .ToListAsync(cancellationToken);
	}

	public Task<int> HandleAsync(TeamCountQuery message, MessageContext context, CancellationToken cancellationToken = default)
	{
		var teams = _context.Set<Team>().AsNoTracking();
		var users = _context.Set<User>().AsNoTracking();

		var query = from team in teams
		            join user in users on team.OwnerId equals user.Id
		            orderby team.Id descending
		            select new TeamListModel
		            {
			            Id = team.Id,
			            Name = team.Name,
			            Description = team.Description,
			            MembersCount = team.MembersCount,
			            CreatedAt = team.CreatedAt,
			            UpdatedAt = team.UpdatedAt,
			            OwnerId = team.OwnerId,
			            OwnerUsername = user.Username,
			            OwnerNickname = user.Nickname,
		            };

		var predicate = PredicateBuilder.True<TeamListModel>();

		if (!_user.IsInRoles(RoleName.Admin))
		{
			var memberTeamIds = _context.Set<TeamMember>()
			                            .AsNoTracking()
			                            .Where(m => m.UserId == _user.UserId)
			                            .Select(m => m.TeamId);
			predicate = predicate.And(t => memberTeamIds.Contains(t.Id));
		}

		predicate = message.Owned switch
		{
			true => predicate.And(t => t.OwnerId == _user.UserId),
			false => predicate.And(t => t.OwnerId != _user.UserId),
			_ => predicate
		};

		if (!string.IsNullOrWhiteSpace(message.Keyword))
		{
			var keyword = message.Keyword.Normalize(TextCaseType.Lower);
			predicate = predicate.And(t => t.Name.ToLower().Contains(keyword) || t.Description.ToLower().Contains(keyword) || t.OwnerUsername.ToLower().Contains(keyword) || t.OwnerNickname.ToLower().Contains(keyword));
		}

		{
		}
		return query.Where(predicate).CountAsync(cancellationToken);
	}

	public Task<TeamDetailModel> HandleAsync(TeamDetailQuery message, MessageContext context, CancellationToken cancellationToken = default)
	{
		var teams = _context.Set<Team>().AsNoTracking();
		var users = _context.Set<User>().AsNoTracking();

		var query = from team in teams
		            join user in users on team.OwnerId equals user.Id
		            where team.Id == message.Id
		            select new TeamDetailModel
		            {
			            Id = team.Id,
			            Name = team.Name,
			            Description = team.Description,
			            MembersCount = team.MembersCount,
			            CreatedAt = team.CreatedAt,
			            UpdatedAt = team.UpdatedAt,
			            OwnerId = team.OwnerId,
			            OwnerUsername = user.Username,
			            OwnerNickname = user.Nickname
		            };

		return query.FirstOrDefaultAsync(cancellationToken);
	}

	public async Task<IList<TeamMemberModel>> HandleAsync(TeamMemberListQuery message, MessageContext context, CancellationToken cancellationToken = default)
	{
		var members = _context.Set<TeamMember>().AsNoTracking();
		var users = _context.Set<User>().AsNoTracking();

		var query = from member in members
		            join user in users on member.UserId equals user.Id
		            where member.TeamId == message.TeamId
		            orderby member.Id descending
		            select new TeamMemberModel
		            {
			            Id = member.Id,
			            TeamId = member.TeamId,
			            UserId = member.UserId,
			            Username = user.Username,
			            Nickname = user.Nickname,
			            Email = user.Email,
			            Phone = user.Phone,
			            JoinedAt = member.CreatedAt,
		            };

		var predicate = PredicateBuilder.True<TeamMemberModel>();
		if (!string.IsNullOrWhiteSpace(message.Keyword))
		{
			var keyword = message.Keyword.Normalize(TextCaseType.Lower);
			predicate = predicate.And(m => m.Username.ToLower().Contains(keyword) || m.Nickname.ToLower().Contains(keyword));
		}

		return await query.Where(predicate)
		                  .OrderByDescending(m => m.Id)
		                  .Skip(message.Skip)
		                  .Take(message.Size)
		                  .ToListAsync(cancellationToken);
	}

	public Task<int> HandleAsync(TeamMemberCountQuery message, MessageContext context, CancellationToken cancellationToken = default)
	{
		var members = _context.Set<TeamMember>().AsNoTracking();
		var users = _context.Set<User>().AsNoTracking();

		var query = from member in members
		            join user in users on member.UserId equals user.Id
		            where member.TeamId == message.TeamId
		            orderby member.Id descending
		            select new TeamMemberModel
		            {
			            Id = member.Id,
			            TeamId = member.TeamId,
			            UserId = member.UserId,
			            Username = user.Username,
			            Nickname = user.Nickname,
			            Email = user.Email,
			            Phone = user.Phone,
			            JoinedAt = member.CreatedAt,
		            };

		var predicate = PredicateBuilder.True<TeamMemberModel>();
		if (!string.IsNullOrWhiteSpace(message.Keyword))
		{
			var keyword = message.Keyword.Normalize(TextCaseType.Lower);
			predicate = predicate.And(m => m.Username.ToLower().Contains(keyword) || m.Nickname.ToLower().Contains(keyword));
		}

		{
		}
		return query.Where(predicate).CountAsync(cancellationToken);
	}
}