using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Domain.Events;

internal class ConfigurationItemCreatedEvent : DomainEvent
{
	public long ConfigurationId { get; set; }

	public long ItemId { get; set; }

	public string Key { get; set; }

	public string Value { get; set; }
}