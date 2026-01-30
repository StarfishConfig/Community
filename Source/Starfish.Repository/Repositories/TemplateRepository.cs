using Microsoft.EntityFrameworkCore;
using Nerosoft.Euonia.Mapping;
using Nerosoft.Starfish.Persistent.Data;
using Nerosoft.Starfish.Persistent.Repositories;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Repository.Specifications;
using Nerosoft.Starfish.Infrastructure;

namespace Nerosoft.Starfish.Repository.Repositories;

internal class TemplateRepository(SystemDataContext context) : ITemplateRepository
{
	public Task DeleteAsync(long id, CancellationToken cancellationToken = default)
	{
		return context.Set<Template>()
		              .Where(x => x.Id == id)
		              .ExecuteUpdateAsync(x => x.SetProperty(y => y.IsDeleted, true), cancellationToken);
	}

	public Task<bool> ExistsDefaultAsync(string code, TemplateType type, long excludeId = 0, CancellationToken cancellationToken = default)
	{
		var specification = TemplateSpecification.ExistsDefault(code, type, excludeId);

		var predicate = specification.Satisfy();

		return context.Set<Template>()
		              .AsNoTracking()
		              .AnyAsync(predicate, cancellationToken);
	}

	public async Task<TemplateData> GetAsync(long id, CancellationToken cancellationToken = default)
	{
		var entity = await context.Set<Template>().FindAsync([id], cancellationToken);
		if (entity is null || entity.IsDeleted)
		{
			throw new KeyNotFoundException($"MessageTemplate with id '{id}' was not found.");
		}

		var data = new TemplateData(entity.Id);
		TypeAdapter.ProjectedAs(entity, data);
		return data;
	}

	public async Task<long> SaveAsync(TemplateData data, CancellationToken cancellationToken = default)
	{
		Template entity;
		if (data.Id <= 0)
		{
			entity = TypeAdapter.ProjectedAs<Template>(data);
			await context.AddAsync(entity, cancellationToken);
		}
		else
		{
			entity = await context.Set<Template>().FindAsync([data.Id], cancellationToken);
			if (entity is null || entity.IsDeleted)
			{
				throw new KeyNotFoundException($"MessageTemplate with id '{data.Id}' was not found.");
			}

			TypeAdapter.ProjectedAs(data, entity);
		}

		await context.SaveChangesAsync(true, cancellationToken);
		return entity.Id;
	}
}