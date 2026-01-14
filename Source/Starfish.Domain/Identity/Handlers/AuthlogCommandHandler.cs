using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Domain.Aggregates;
using Nerosoft.Starfish.Domain.Commands;

namespace Nerosoft.Starfish.Domain.Handlers;

internal class AuthlogCommandHandler(IServiceProvider provider)
	: CommandHandlerBase(provider),
	  IHandler<AuthlogCreateCommand>
{
	public Task HandleAsync(AuthlogCreateCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<Authlog>()
		               .Create(message, cancellationToken)
		               .ExecuteAsync(cancellationToken);
	}
}