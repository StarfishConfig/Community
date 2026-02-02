using Nerosoft.Euonia.Application;
using Nerosoft.Euonia.Mapping;
using Nerosoft.Starfish.Domain.Commands;
using Nerosoft.Starfish.Facade.Interfaces;
using Nerosoft.Starfish.Repository.Requests;
using Nerosoft.Starfish.Shared;
using Nerosoft.Starfish.Transit;

namespace Nerosoft.Starfish.Facade.Implements;

internal class ConfigurationApplicationService : BaseApplicationService, IConfigurationApplicationService
{
	public async Task<List<ConfigurationListDto>> SearchAsync(string keyword, long teamId, int skip, int size, CancellationToken cancellationToken = default)
	{
		IList<long> teamScopes;

		if (User.IsInRoles(RoleName.Admin))
		{
			teamScopes = [];
		}
		else
		{
			teamScopes = await GetUserTeamScopeAsync(TeamMemberRole.None, cancellationToken);
		}

		var request = new ConfigurationSearchQuery(teamScopes, keyword, teamId, skip, size);

		var results = await Bus.CallAsync(request, cancellationToken);
		var items = TypeAdapter.ProjectedAs<List<ConfigurationListDto>>(results);

		var teamIds = items.Select(x => x.TeamId).Distinct().ToList();
		var teamInfos = await Bus.CallAsync(new TeamLookupQuery(teamIds), cancellationToken);

		foreach (var dto in items)
		{
			dto.TeamName = teamInfos.TryGetValue(dto.TeamId, out var teamName) ? teamName : "--";
		}

		return items;
	}

	public async Task<int> CountAsync(string keyword, long teamId, CancellationToken cancellationToken = default)
	{
		IList<long> teamScopes;

		if (User.IsInRoles(RoleName.Admin))
		{
			teamScopes = [];
		}
		else
		{
			teamScopes = await GetUserTeamScopeAsync(TeamMemberRole.None, cancellationToken);
		}

		return await Bus.CallAsync(new ConfigurationCountQuery(teamScopes, keyword, teamId), cancellationToken);
	}

	public async Task<ConfigurationDetailDto> GetAsync(long id, CancellationToken cancellationToken = default)
	{
		IList<long> teamScopes;

		if (User.IsInRoles(RoleName.Admin))
		{
			teamScopes = [];
		}
		else
		{
			teamScopes = await GetUserTeamScopeAsync(TeamMemberRole.None, cancellationToken);
		}

		var request = new ConfigurationDetailQuery(teamScopes, id);
		var result = await Bus.CallAsync(request, cancellationToken);
		return TypeAdapter.ProjectedAs<ConfigurationDetailDto>(result);
	}

	public async Task CreateAsync(ConfigurationCreateDto data, CancellationToken cancellationToken = default)
	{
		if (data.TeamId <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(data.TeamId));
		}

		var team = await Bus.CallAsync(new TeamBaseInfoQuery(data.TeamId, false), cancellationToken);

		if (team == null)
		{
			throw new InvalidOperationException("Team not found.");
		}

		var isTeamOwner = string.Equals(team.OwnerId, User.UserId);

		if (!isTeamOwner)
		{
			throw new ForbiddenException("Only the team owner can create configurations.");
		}

		var command = TypeAdapter.ProjectedAs<ConfigurationCreateCommand>(data);

		await Bus.SendAsync(command, cancellationToken);
	}

	public Task UpdateAsync(long id, ConfigurationUpdateDto data, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
	{
		var teamScope = await GetUserTeamScopeAsync(TeamMemberRole.None, cancellationToken);

		var baseInfo = await Bus.CallAsync(new ConfigurationBaseInfoQuery(teamScope, id), cancellationToken);

		if (baseInfo == null)
		{
			throw new InvalidOperationException("Configuration not found.");
		}

		var isTeamOwner = await Bus.CallAsync(new TeamOwnerCheckQuery(baseInfo.TeamId, User.UserId), cancellationToken);

		if (!isTeamOwner)
		{
			throw new ForbiddenException("Only the team owner can delete configurations.");
		}

		var command = new ConfigurationDeleteCommand(id);

		await Bus.SendAsync(command, cancellationToken);
	}

	private Task<IList<long>> GetUserTeamScopeAsync(TeamMemberRole role, CancellationToken cancellationToken)
	{
		return Bus.CallAsync(new TeamScopeQuery(User.UserId, role), cancellationToken);
	}
}