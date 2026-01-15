using Nerosoft.Starfish.Persistent;

namespace Nerosoft.Starfish.Domain.Repositories;

internal interface IAuthlogRepository
{
	Task SaveAsync(AuthlogData data, CancellationToken cancellationToken = default);
}