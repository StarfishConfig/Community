using Nerosoft.Starfish.Persistent;

namespace Nerosoft.Starfish.Domain.Repositories;

internal interface ITokenRepository : IRepository
{
	Task SaveAsync(TokenData data, CancellationToken cancellationToken = default);

	Task DeleteAsync(string type, string key, CancellationToken cancellationToken = default);
}