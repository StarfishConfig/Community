using Nerosoft.Euonia.Application;
using Nerosoft.Starfish.Facade.Transit;

namespace Nerosoft.Starfish.Facade.Interfaces;

public interface ITeamApplicationService : IApplicationService
{
	Task CreateAsync(TeamCreateDto data, CancellationToken cancellationToken = default);

	Task UpdateAsync(string id, TeamUpdateDto data, CancellationToken cancellationToken = default);
}