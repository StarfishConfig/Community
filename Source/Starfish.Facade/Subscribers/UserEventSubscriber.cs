using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Facade.Events;

namespace Nerosoft.Starfish.Facade.Subscribers;

internal sealed class UserEventSubscriber : IHandler<UserAuthSuccessEvent>, IHandler<UserAuthFailureEvent>
{
	public Task HandleAsync(UserAuthSuccessEvent message, MessageContext context, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task HandleAsync(UserAuthFailureEvent message, MessageContext context, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
}