using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Domain.Events;

/// <summary>
/// Event triggered when a new configuration is created.
/// </summary>
public class ConfigurationCreatedEvent : DomainEvent
{
	/// <summary>
	/// Gets or sets the unique identifier of the created configuration.
	/// </summary>
	public long Id { get; init; }

	/// <summary>
	/// Gets the unique identifier for the team.
	/// </summary>
	/// <remarks>
	/// This property is set during object initialization and cannot be modified afterwards.
	/// </remarks>
	public long TeamId { get; init; }

	/// <summary>
	/// Gets the unique code that identifies the entity.
	/// </summary>
	public string Code { get; init; }

	/// <summary>
	/// Gets the name associated with this instance. This property is initialized during object creation and is read-only
	/// thereafter.
	/// </summary>
	public string Name { get; init; }
}