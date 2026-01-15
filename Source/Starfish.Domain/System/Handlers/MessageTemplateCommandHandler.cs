using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Domain.Aggregates;
using Nerosoft.Starfish.Domain.Commands;

namespace Nerosoft.Starfish.Domain.Handlers;

internal class MessageTemplateCommandHandler(IServiceProvider provider)
	: CommandHandlerBase(provider),
	IHandler<MessageTemplateCreateCommand>,
	IHandler<MessageTemplateUpdateCommand>,
	IHandler<MessageTemplateDeleteCommand>
{
	public Task HandleAsync(MessageTemplateCreateCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<MessageTemplate>()
			.Create(message, cancellationToken)
			.ExecuteAsync()
			.NextAsync(target =>
			{
				context.Response(target.Id);
			});
	}

	public Task HandleAsync(MessageTemplateUpdateCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<MessageTemplate>()
			.Fetch(message.Id, cancellationToken)
			.Handle(target =>
			{
				target.Name = message.Name;
				target.Code = message.Code;
				target.Language = message.Language;
				target.Type = message.Type;
				target.Subject = message.Subject;
				target.Body = message.Body;
				target.Default = message.Default;
			})
			.ExecuteAsync();
	}

	public Task HandleAsync(MessageTemplateDeleteCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<MessageTemplate>()
			.Delete(message.Id, cancellationToken)
			.ExecuteAsync();
	}
}
