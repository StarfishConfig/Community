using Nerosoft.Starfish.Persistent;

namespace Nerosoft.Starfish.Domain.Repositories;

public interface IAuthlogRepository
{
	Task SaveAsync(AuthlogData data, CancellationToken cancellationToken = default);
}