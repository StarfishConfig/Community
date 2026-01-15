using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Domain.Commands;
using Nerosoft.Starfish.Facade.Events;

namespace Nerosoft.Starfish.Facade.Subscribers;

internal sealed class UserEventSubscriber(IBus bus)
	: IHandler<UserAuthSuccessEvent>,
	  IHandler<UserAuthFailureEvent>
{
	public Task HandleAsync(UserAuthSuccessEvent message, MessageContext context, CancellationToken cancellationToken = default)
	{
		var command = new UserFailedCountChangeCommand(message.UserId, "-");
		return bus.SendAsync(command, cancellationToken);
	}

	public Task HandleAsync(UserAuthFailureEvent message, MessageContext context, CancellationToken cancellationToken = default)
	{
		if (string.IsNullOrWhiteSpace(message.UserId) || message.Data?.GetValueOrDefault("Locked", string.Empty) == "true")
		{
			return Task.CompletedTask;
		}

		var command = new UserFailedCountChangeCommand(message.UserId, "+");
		return bus.SendAsync(command, cancellationToken);
	}
}