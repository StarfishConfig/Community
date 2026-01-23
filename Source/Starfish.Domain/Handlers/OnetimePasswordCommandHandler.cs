using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Domain.Aggregates;
using Nerosoft.Starfish.Domain.Commands;

namespace Nerosoft.Starfish.Domain.Handlers;

internal class OnetimePasswordCommandHandler(IServiceProvider provider)
	: CommandHandlerBase(provider),
	IHandler<OnetimePasswordCreateCommand>,
	IHandler<OnetimePasswordCheckOffCommand>,
	IHandler<OnetimePasswordDeleteCommand>
{
	public Task HandleAsync(OnetimePasswordCreateCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<OnetimePassword>()
			.Create(message, cancellationToken)
			.ExecuteAsync(cancellationToken);
	}

	public Task HandleAsync(OnetimePasswordCheckOffCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<OnetimePassword>()
			.Fetch(message.RequestId, cancellationToken)
			.Handle(target =>
			{
				target.CheckOff(message.CheckedAt);
				target.MarkAsChanged();
			})
			.ExecuteAsync(cancellationToken);
	}

	public Task HandleAsync(OnetimePasswordDeleteCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<OnetimePassword>()
			.Delete(message.RequestId, cancellationToken)
			.ExecuteAsync(cancellationToken);
	}
}
