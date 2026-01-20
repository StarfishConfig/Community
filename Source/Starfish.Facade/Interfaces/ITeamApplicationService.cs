using Nerosoft.Euonia.Application;
using Nerosoft.Starfish.Facade.Transit;

namespace Nerosoft.Starfish.Facade.Interfaces;

public interface ITeamApplicationService : IApplicationService
{
	Task<List<TeamListDto>> SearchAsync(string keyword, int? type, int skip, int size, CancellationToken cancellationToken = default);

	Task<int> CountAsync(string keyword, int? type, CancellationToken cancellationToken = default);

	Task<TeamDetailDto> GetAsync(long id, CancellationToken cancellationToken = default);

	Task<List<TeamMemberDto>> GetMemberAsync(long teamId, string keyword, int skip, int size, CancellationToken cancellationToken = default);

	Task CreateAsync(TeamEditDto data, CancellationToken cancellationToken = default);

	Task UpdateAsync(long id, TeamEditDto data, CancellationToken cancellationToken = default);
}