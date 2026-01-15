using Microsoft.EntityFrameworkCore;
using Nerosoft.Euonia.Bus;
using Nerosoft.Euonia.Mapping;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Repository.Models;
using Nerosoft.Starfish.Repository.Requests;
using Nerosoft.Starfish.Repository.Specifications;

namespace Nerosoft.Starfish.Repository.Handlers;

internal class MessageTemplateQueryHandler : IHandler<MessageTemplateDetailQuery, MessageTemplateDetailModel>,
	IHandler<MessageTemplateMatchQuery, MessageTemplateDetailModel>,
	IHandler<MessageTemplateListQuery, IList<MessageTemplateListModel>>
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

		if (entities?.Any() != true)
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

	public Task<IList<MessageTemplateListModel>> HandleAsync(MessageTemplateListQuery message, MessageContext context, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
}
