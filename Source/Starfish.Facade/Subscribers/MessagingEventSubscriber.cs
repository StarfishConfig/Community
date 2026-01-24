using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Facade.Events;

namespace Nerosoft.Starfish.Facade.Subscribers;

internal class MessagingEventSubscriber : IHandler<OnetimePasswordRequestedEventDto>
{
	public Task HandleAsync(OnetimePasswordRequestedEventDto message, MessageContext context, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
}
