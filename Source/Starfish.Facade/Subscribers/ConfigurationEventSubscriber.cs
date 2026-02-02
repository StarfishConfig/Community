using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Facade.Events;

namespace Nerosoft.Starfish.Facade.Subscribers;

internal class ConfigurationEventSubscriber(IServiceProvider provider)
	: IHandler<TeamMemberRemovedEvent>
{
	public Task HandleAsync(TeamMemberRemovedEvent message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Task.CompletedTask;
	}
}