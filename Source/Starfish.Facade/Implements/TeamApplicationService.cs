using Nerosoft.Euonia.Application;
using Nerosoft.Euonia.Mapping;
using Nerosoft.Starfish.Domain.Commands;
using Nerosoft.Starfish.Facade.Interfaces;
using Nerosoft.Starfish.Repository.Requests;
using Nerosoft.Starfish.Shared;
using Nerosoft.Starfish.Transit;

namespace Nerosoft.Starfish.Facade.Implements;

internal class TeamApplicationService : BaseApplicationService, ITeamApplicationService
{
	public Task<List<TeamListDto>> SearchAsync(string keyword, TeamMemberRole role, int skip, int size, CancellationToken cancellationToken = default)
	{
		var request = new TeamSearchQuery(keyword, role, skip, size);
		return Bus.CallAsync(request, cancellationToken)
		          .ContinueWith(task =>
		          {
			          task.WaitAndUnwrapException(cancellationToken);

			          return TypeAdapter.ProjectedAs<List<TeamListDto>>(task.Result);
		          }, cancellationToken);
	}

	public Task<int> CountAsync(string keyword, TeamMemberRole role, CancellationToken cancellationToken = default)
	{
		var request = new TeamCountQuery(keyword, role);
		return Bus.CallAsync(request, cancellationToken);
	}

	public Task<TeamDetailDto> GetAsync(long id, CancellationToken cancellationToken = default)
	{
		var request = new TeamDetailQuery(id);
		return Bus.CallAsync(request, cancellationToken)
		          .ContinueWith(task =>
		          {
			          task.WaitAndUnwrapException(cancellationToken);

			          return TypeAdapter.ProjectedAs<TeamDetailDto>(task.Result);
		          }, cancellationToken);
	}

	public Task<List<TeamMemberDto>> GetMemberListAsync(long teamId, string keyword, int skip, int size, CancellationToken cancellationToken = default)
	{
		var request = new TeamMemberListQuery(teamId, keyword, skip, size);
		return Bus.CallAsync(request, cancellationToken)
		          .ContinueWith(task =>
		          {
			          task.WaitAndUnwrapException(cancellationToken);

			          var members = TypeAdapter.ProjectedAs<List<TeamMemberDto>>(task.Result);
			          return members;
		          }, cancellationToken);
	}

	public Task<int> GetMemberCountAsync(long teamId, string keyword, CancellationToken cancellationToken = default)
	{
		var request = new TeamMemberCountQuery(teamId, keyword);
		return Bus.CallAsync(request, cancellationToken);
	}

	public Task CreateAsync(TeamEditDto data, CancellationToken cancellationToken = default)
	{
		var command = TypeAdapter.ProjectedAs<TeamCreateCommand>(data);
		return Bus.SendAsync(command, cancellationToken);
	}

	public Task UpdateAsync(long id, TeamEditDto data, CancellationToken cancellationToken = default)
	{
		var command = new TeamUpdateCommand(id);
		TypeAdapter.ProjectedAs(data, command);
		return Bus.SendAsync(command, cancellationToken);
	}

	public Task DeleteAsync(long id, CancellationToken cancellationToken = default)
	{
		var command = new TeamDeleteCommand(id);
		return Bus.SendAsync(command, cancellationToken);
	}

	public Task TransferAsync(long id, TeamTransferDto data, CancellationToken cancellationToken = default)
	{
		var command = new TeamTransferCommand(id, data.UserId)
		{
			LeaveAfterTransfer = data.LeaveAfterTransfer
		};
		return Bus.SendAsync(command, cancellationToken);
	}

	public Task AppendMemberAsync(long teamId, IList<string> userIds, CancellationToken cancellationToken = default)
	{
		var command = new TeamMemberAppendCommand(teamId, userIds);
		return Bus.SendAsync(command, cancellationToken);
	}

	public Task RemoveMemberAsync(long teamId, IList<string> userIds, CancellationToken cancellationToken = default)
	{
		var command = new TeamMemberRemoveCommand(teamId, userIds);
		return Bus.SendAsync(command, cancellationToken);
	}
}