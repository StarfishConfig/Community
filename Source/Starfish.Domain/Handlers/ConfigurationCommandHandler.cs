using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Domain.Aggregates;
using Nerosoft.Starfish.Domain.Commands;

namespace Nerosoft.Starfish.Domain.Handlers;

internal sealed class ConfigurationCommandHandler(IServiceProvider provider)
	: CommandHandlerBase(provider),
	  IHandler<ConfigurationCreateCommand>,
	  IHandler<ConfigurationUpdateCommand>,
	  IHandler<ConfigurationDeleteCommand>
{
	public Task HandleAsync(ConfigurationCreateCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<Configuration>()
		               .Create(message, cancellationToken)
		               .ExecuteAsync(cancellationToken);
	}

	public Task HandleAsync(ConfigurationUpdateCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<Configuration>()
		               .Fetch(message.Id, cancellationToken)
		               .Handle(target =>
		               {
			               target.SetCode(message.Code);
			               target.SetName(message.Name);
			               target.SetDescription(message.Description);
		               })
		               .ExecuteAsync(cancellationToken);
	}

	public Task HandleAsync(ConfigurationDeleteCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<Configuration>()
		               .Delete(message.Id, cancellationToken)
		               .ExecuteAsync(cancellationToken);
	}
}