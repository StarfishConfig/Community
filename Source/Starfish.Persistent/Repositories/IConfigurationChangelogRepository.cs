using Nerosoft.Starfish.Persistent.Data;

namespace Nerosoft.Starfish.Persistent.Repositories;

internal interface IConfigurationChangelogRepository : IRepository
{
	Task SaveAsync(ConfigurationChangelogData data, CancellationToken cancellationToken = default);
}