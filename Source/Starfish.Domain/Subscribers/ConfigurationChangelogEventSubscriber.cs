using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Domain.Aggregates;
using Nerosoft.Starfish.Domain.Events;

namespace Nerosoft.Starfish.Domain.Subscribers;

internal class ConfigurationChangelogEventSubscriber(IServiceProvider provider)
	: IHandler<ConfigurationCreatedEvent>,
	  IHandler<ConfigurationItemCreatedEvent>,
	  IHandler<ConfigurationItemUpdatedEvent>,
	  IHandler<ConfigurationItemDeletedEvent>
{
	public Task HandleAsync(ConfigurationCreatedEvent message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<ConfigurationChangelog>(provider)
					   .Create(cancellationToken)
					   .Handle(target =>
					   {
						   target.ConfigurationId = message.Id;
						   target.Key = message.Code;
						   target.Value = message.Name;
						   target.ChangeType = "create";
						   target.ChangedBy = context.User.Identity.Name;
						   target.ChangedAt = DateTime.UtcNow;
					   })
					   .ExecuteAsync(cancellationToken);
	}

	public Task HandleAsync(ConfigurationItemCreatedEvent message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<ConfigurationChangelog>(provider)
					   .Create(cancellationToken)
					   .Handle(target =>
					   {
						   target.ConfigurationId = message.ConfigurationId;
					   })
					   .ExecuteAsync(cancellationToken);
	}

	public Task HandleAsync(ConfigurationItemUpdatedEvent message, MessageContext context, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task HandleAsync(ConfigurationItemDeletedEvent message, MessageContext context, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
}