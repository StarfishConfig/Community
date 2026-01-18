using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Domain.Aggregates;
using Nerosoft.Starfish.Domain.Commands;

namespace Nerosoft.Starfish.Domain.Handlers;

internal class TemplateCommandHandler(IServiceProvider provider)
	: CommandHandlerBase(provider),
	  IHandler<TemplateCreateCommand>,
	  IHandler<TemplateUpdateCommand>,
	  IHandler<TemplateDeleteCommand>
{
	public Task HandleAsync(TemplateCreateCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<Template>()
		               .Create(message, cancellationToken)
		               .ExecuteAsync(cancellationToken)
		               .NextAsync(target =>
		               {
			               context.Response(target.Id);
		               });
	}

	public Task HandleAsync(TemplateUpdateCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<Template>()
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
		               .ExecuteAsync(cancellationToken);
	}

	public Task HandleAsync(TemplateDeleteCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<Template>()
		               .Delete(message.Id, cancellationToken)
		               .ExecuteAsync(cancellationToken);
	}
}