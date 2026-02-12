using Nerosoft.Euonia.Mapping;
using Nerosoft.Starfish.Persistent.Data;
using Nerosoft.Starfish.Persistent.Repositories;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Repositories;

internal class ConfigurationChangelogRepository(ProjectDataContext context) : IConfigurationChangelogRepository
{
	public async Task SaveAsync(ConfigurationChangelogData data, CancellationToken cancellationToken = default)
	{
		var entity = TypeAdapter.ProjectedAs<ConfigurationChangelog>(data);
		await context.AddAsync(entity, cancellationToken);
		await context.SaveChangesAsync(true, cancellationToken);
	}
}