using Microsoft.EntityFrameworkCore;
using Nerosoft.Euonia.Bus;
using Nerosoft.Euonia.Linq;
using Nerosoft.Euonia.Mapping;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Repository.Models;
using Nerosoft.Starfish.Repository.Requests;
using Nerosoft.Starfish.Repository.Specifications;

namespace Nerosoft.Starfish.Repository.Handlers;

internal class MessageTemplateQueryHandler : IHandler<MessageTemplateDetailQuery, MessageTemplateDetailModel>,
                                             IHandler<MessageTemplateMatchQuery, MessageTemplateDetailModel>,
                                             IHandler<MessageTemplateListQuery, IList<MessageTemplateListModel>>,
                                             IHandler<MessageTemplateCountQuery, int>
{
	private readonly SystemDataContext _context;

	public MessageTemplateQueryHandler(SystemDataContext context)
	{
		_context = context;
	}

	public async Task<MessageTemplateDetailModel> HandleAsync(MessageTemplateDetailQuery message, MessageContext context, CancellationToken cancellationToken = default)
	{
		var entity = await _context.Set<MessageTemplate>()
		                           .FindAsync([message.Id], cancellationToken);

		if (entity == null)
		{
			throw new NotFoundException();
		}

		return TypeAdapter.ProjectedAs<MessageTemplateDetailModel>(entity);
	}

	public async Task<MessageTemplateDetailModel> HandleAsync(MessageTemplateMatchQuery message, MessageContext context, CancellationToken cancellationToken = default)
	{
		var specification = MessageTemplateSpecification.Matches(message.Code, message.Type, message.Language);
		var predicate = specification.Satisfy();

		var entities = await _context.Set<MessageTemplate>()
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
		}, value => value != null, default(MessageTemplate));

		return TypeAdapter.ProjectedAs<MessageTemplateDetailModel>(entity);
	}

	public async Task<IList<MessageTemplateListModel>> HandleAsync(MessageTemplateListQuery message, MessageContext context, CancellationToken cancellationToken = default)
	{
		var specification = MessageTemplateSpecification.True()
		                                                .AndIf(!string.IsNullOrEmpty(message.Code), () => MessageTemplateSpecification.CodeContains(message.Code))
		                                                .AndIf(message.Type.HasValue, () => MessageTemplateSpecification.TypeEquals(message.Type!.Value))
		                                                .AndIf(!string.IsNullOrWhiteSpace(message.Keyword), () => MessageTemplateSpecification.NameContains(message.Keyword));

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
		var query = _context.Set<MessageTemplate>()
		                    .AsNoTracking()
		                    .Where(predicate)
		                    .OrderByDescending(t => t.CreatedAt)
		                    .Skip(message.Skip)
		                    .Take(message.Size);
		var entities = await query.ToListAsync(cancellationToken);
		return TypeAdapter.ProjectedAs<IList<MessageTemplateListModel>>(entities);
	}

	public Task<int> HandleAsync(MessageTemplateCountQuery message, MessageContext context, CancellationToken cancellationToken = new CancellationToken())
	{
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

		var query = _context.Set<MessageTemplate>()
		                    .AsNoTracking()
		                    .Where(predicate);
		return query.CountAsync(cancellationToken);
	}
}