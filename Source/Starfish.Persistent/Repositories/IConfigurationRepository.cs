using Nerosoft.Starfish.Persistent.Data;

namespace Nerosoft.Starfish.Persistent.Repositories;

internal interface IConfigurationRepository : IRepository
{
	Task<long> SaveAsync(ConfigurationData data, CancellationToken cancellationToken = default);

	Task<ConfigurationData> GetAsync(long id, CancellationToken cancellationToken = default);

	Task<bool> CheckCodeExistsAsync(long teamId, string code, long excludeId = 0, CancellationToken cancellationToken = default);
}