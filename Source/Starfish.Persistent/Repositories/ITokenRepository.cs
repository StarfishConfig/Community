using Nerosoft.Starfish.Persistent;

namespace Nerosoft.Starfish.Domain.Repositories;

internal interface ITokenRepository
{
	Task SaveAsync(TokenData data, CancellationToken cancellationToken = default);

	Task DeleteAsync(string type, string key, CancellationToken cancellationToken = default);
}