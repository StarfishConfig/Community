using Microsoft.EntityFrameworkCore;
using Nerosoft.Euonia.Bus;
using Nerosoft.Euonia.Linq;
using Nerosoft.Euonia.Mapping;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Repository.Models;
using Nerosoft.Starfish.Repository.Requests;
using Nerosoft.Starfish.Repository.Specifications;

namespace Nerosoft.Starfish.Repository.Handlers;

internal class TemplateQueryHandler : IHandler<TemplateDetailQuery, TemplateDetailModel>,
                                             IHandler<TemplateMatchQuery, TemplateDetailModel>,
                                             IHandler<TemplateListQuery, IList<TemplateListModel>>,
                                             IHandler<TemplateCountQuery, int>
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

	public async Task<IList<TemplateListModel>> HandleAsync(TemplateListQuery message, MessageContext context, CancellationToken cancellationToken = default)
	{
		var specification = TemplateSpecification.True()
		                                                .AndIf(!string.IsNullOrEmpty(message.Code), () => TemplateSpecification.CodeContains(message.Code))
		                                                .AndIf(message.Type.HasValue, () => TemplateSpecification.TypeEquals(message.Type!.Value))
		                                                .AndIf(!string.IsNullOrWhiteSpace(message.Keyword), () => TemplateSpecification.NameContains(message.Keyword));

		/*
		var specifications = new List<ISpecification<MessageTemplate>>()
		{
			MessageTemplateSpecification.IdNotEquals(0)
		};
		if (!string.IsNullOrEmpty(message.Code))
		{
			specifications.Add(MessageTemplateSpecification.CodeContains(message.Code));
		}

		if (message.Type.HasValue)
		{
			specifications.Add(MessageTemplateSpecification.TypeEquals(message.Type.Value));
		}

		if (!string.IsNullOrWhiteSpace(message.Keyword))
		{
			specifications.Add(MessageTemplateSpecification.NameContains(message.Keyword));
		}

		var predicate = new CompositeSpecification<MessageTemplate>(PredicateOperator.AndAlso, specifications.ToArray());
		*/
		
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

	public Task<int> HandleAsync(TemplateCountQuery message, MessageContext context, CancellationToken cancellationToken = new CancellationToken())
	{
		var specifications = new List<ISpecification<Template>>()
		{
			TemplateSpecification.IdNotEquals(0)
		};
		if (!string.IsNullOrEmpty(message.Code))
		{
			specifications.Add(TemplateSpecification.CodeContains(message.Code));
		}

		if (message.Type.HasValue)
		{
			specifications.Add(TemplateSpecification.TypeEquals(message.Type.Value));
		}

		if (!string.IsNullOrWhiteSpace(message.Keyword))
		{
			specifications.Add(TemplateSpecification.NameContains(message.Keyword));
		}

		var predicate = new CompositeSpecification<Template>(PredicateOperator.AndAlso, specifications.ToArray());

		var query = _context.Set<Template>()
		                    .AsNoTracking()
		                    .Where(predicate);
		return query.CountAsync(cancellationToken);
	}
}