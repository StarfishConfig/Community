using Nerosoft.Euonia.Application;
using Nerosoft.Euonia.Mapping;
using Nerosoft.Starfish.Domain.Commands;
using Nerosoft.Starfish.Facade.Interfaces;
using Nerosoft.Starfish.Facade.Transit;

namespace Nerosoft.Starfish.Facade.Implements;

internal class TeamApplicationService : BaseApplicationService, ITeamApplicationService
{
	public Task<List<TeamListDto>> SearchAsync(string keyword, int? type, int skip, int size, CancellationToken cancellationToken = default)
	{
		var request = new TeamSearchQuery(keyword, type, skip, size);
		return Bus.CallAsync(request, cancellationToken)
		          .ContinueWith(task =>
		          {
			          task.WaitAndUnwrapException(cancellationToken);

			          return TypeAdapter.ProjectedAs<List<TeamListDto>>(task.Result);
		          }, cancellationToken);
	}

	public Task<int> CountAsync(string keyword, int? type, CancellationToken cancellationToken = default)
	{
		var request = new TeamCountQuery(keyword, type);
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

	public Task<List<TeamMemberDto>> GetMemberAsync(long teamId, string keyword, int skip, int size, CancellationToken cancellationToken = default)
	{
		var request = new TeamMemberQuery(teamId, keyword, skip, size);
		return Bus.CallAsync(request, cancellationToken)
		          .ContinueWith(task =>
		          {
			          task.WaitAndUnwrapException(cancellationToken);

			          var members = TypeAdapter.ProjectedAs<List<TeamMemberDto>>(task.Result);
			          return members;
		          }, cancellationToken);
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
}