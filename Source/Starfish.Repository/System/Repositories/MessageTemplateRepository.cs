using Microsoft.EntityFrameworkCore;
using Nerosoft.Euonia.Mapping;
using Nerosoft.Starfish.Persistent.Data;
using Nerosoft.Starfish.Persistent.Repositories;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Repository.Specifications;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Repositories;

internal class MessageTemplateRepository(SystemDataContext context) : IMessageTemplateRepository, ITransientDependency
{
	public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<bool> ExistsDefaultAsync(string code, MessageTemplateType type, string excludeId = null, CancellationToken cancellationToken = default)
	{
		var specification = MessageTemplateSpecification.ExistsDefault(code, type, excludeId);

		var predicate = specification.Satisfy();

		return context.Set<MessageTemplate>()
				.AsNoTracking()
				.AnyAsync(predicate, cancellationToken);
	}

	public async Task<MessageTemplateData> GetAsync(string id, CancellationToken cancellationToken = default)
	{
		var entity = await context.Set<MessageTemplate>().FindAsync([id], cancellationToken);
		if (entity is null || entity.IsDeleted)
		{
			throw new KeyNotFoundException($"MessageTemplate with id '{id}' was not found.");
		}

		var data = new MessageTemplateData(entity.Id);
		TypeAdapter.ProjectedAs(entity, data);
		return data;
	}

	public async Task<string> SaveAsync(MessageTemplateData data, CancellationToken cancellationToken = default)
	{
		MessageTemplate entity;
		if (string.IsNullOrEmpty(data.Id))
		{
			entity = TypeAdapter.ProjectedAs<MessageTemplate>(data);
			await context.AddAsync(entity, cancellationToken);
		}
		else
		{
			entity = await context.Set<MessageTemplate>().FindAsync([data.Id], cancellationToken);
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
