namespace Nerosoft.Starfish.Persistent.Repositories;

internal interface IConfigurationRepository : IRepository
{
	Task<bool> CheckCodeExistsAsync(long teamId, string code, long excludeId = 0, CancellationToken cancellationToken = default);
}