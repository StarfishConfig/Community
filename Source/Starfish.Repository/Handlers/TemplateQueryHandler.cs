using Microsoft.EntityFrameworkCore;
using Nerosoft.Euonia.Bus;
using Nerosoft.Euonia.Linq;
using Nerosoft.Euonia.Mapping;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Repository.Models;
using Nerosoft.Starfish.Repository.Requests;
using Nerosoft.Starfish.Repository.Specifications;
using Nerosoft.Starfish.Infrastructure;

namespace Nerosoft.Starfish.Repository.Handlers;

internal class TemplateQueryHandler : IHandler<TemplateDetailQuery, TemplateDetailModel>,
                                      IHandler<TemplateMatchQuery, TemplateDetailModel>,
                                      IHandler<TemplateSearchRequest, IList<TemplateListModel>>,
                                      IHandler<TemplateCountRequest, int>
{
	private readonly SystemDataContext _context;

	public TemplateQueryHandler(SystemDataContext context)
	{
		_context = context;
	}

	public async Task<TemplateDetailModel> HandleAsync(TemplateDetailQuery message, MessageContext context, CancellationToken cancellationToken = default)
	{
		var entity = await _context.Set<Template>()
		                           .FindAsync([message.Id], cancellationToken);

		if (entity == null)
		{
			throw new NotFoundException();
		}

		return TypeAdapter.ProjectedAs<TemplateDetailModel>(entity);
	}

	public async Task<TemplateDetailModel> HandleAsync(TemplateMatchQuery message, MessageContext context, CancellationToken cancellationToken = default)
	{
		var specification = TemplateSpecification.Matches(message.Code, message.Type, message.Language);
		var predicate = specification.Satisfy();

		var entities = await _context.Set<Template>()
		                             .AsNoTracking()
		                             .Where(predicate)
		                             .ToListAsync(cancellationToken);

		if (entities.Count == 0)
		{
			return null;
		}

		var entity = PriorityValueFinder.Find(queue =>
		{
			queue.Enqueue(() => entities.FirstOrDefault(e => e.Language.Equals(message.Language, StringComparison.OrdinalIgnoreCase)), 0);
			queue.Enqueue(() => entities.FirstOrDefault(e => e.Default), 2);
			queue.Enqueue(() => entities.FirstOrDefault(), 3);
		}, value => value != null, default(Template));

		return TypeAdapter.ProjectedAs<TemplateDetailModel>(entity);
	}

	public async Task<IList<TemplateListModel>> HandleAsync(TemplateSearchRequest message, MessageContext context, CancellationToken cancellationToken = default)
	{
		var specification = TemplateSpecification.True()
		                                         .AndIf(message.Type > TemplateType.None, () => TemplateSpecification.TypeEquals(message.Type!.Value))
		                                         .AndIf(!string.IsNullOrWhiteSpace(message.Keyword), () => TemplateSpecification.ContainsKeyword(message.Keyword));
		
		var predicate = specification.Satisfy();
		var query = _context.Set<Template>()
		                    .AsNoTracking()
		                    .Where(predicate)
		                    .OrderByDescending(t => t.CreatedAt)
		                    .Skip(message.Skip)
		                    .Take(message.Size);
		var entities = await query.ToListAsync(cancellationToken);
		return TypeAdapter.ProjectedAs<IList<TemplateListModel>>(entities);
	}

	public Task<int> HandleAsync(TemplateCountRequest message, MessageContext context, CancellationToken cancellationToken = default)
	{
		var specification = TemplateSpecification.True()
		                                         .AndIf(message.Type > TemplateType.None, () => TemplateSpecification.TypeEquals(message.Type!.Value))
		                                         .AndIf(!string.IsNullOrWhiteSpace(message.Keyword), () => TemplateSpecification.ContainsKeyword(message.Keyword));

		var predicate = specification.Satisfy();

		var query = _context.Set<Template>()
		                    .AsNoTracking()
		                    .Where(predicate);
		return query.CountAsync(cancellationToken);
	}
}