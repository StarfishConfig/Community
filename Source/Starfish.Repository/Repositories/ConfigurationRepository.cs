using Nerosoft.Euonia.Mapping;
using Nerosoft.Starfish.Persistent.Data;
using Nerosoft.Starfish.Persistent.Repositories;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Repositories;

internal class ConfigurationRepository(ProjectDataContext context) : IConfigurationRepository
{
	public async Task<long> SaveAsync(ConfigurationData data, CancellationToken cancellationToken = default)
	{
		Configuration entity;

		if (data.Id > 0)
		{
			entity = await context.Set<Configuration>().FindAsync([data.Id], cancellationToken);
			TypeAdapter.ProjectedAs(data, entity);
		}
		else
		{
			entity = TypeAdapter.ProjectedAs<Configuration>(data);
			await context.Set<Configuration>().AddAsync(entity, cancellationToken);
		}

		await context.SaveChangesAsync(true, cancellationToken);
		return entity!.Id;
	}

	public async Task<ConfigurationData> GetAsync(long id, CancellationToken cancellationToken = default)
	{
		var entity = await context.Set<Configuration>().FindAsync([id], cancellationToken);

		return TypeAdapter.ProjectedAs<ConfigurationData>(entity);
	}

	public Task<bool> CheckCodeExistsAsync(long teamId, string code, long excludeId = 0, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
}