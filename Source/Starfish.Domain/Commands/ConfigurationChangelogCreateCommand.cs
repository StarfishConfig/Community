using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Domain.Commands;

/// <summary>
/// Command to request creation of a configuration changelog entry.
/// </summary>
/// <remarks>
/// Typically used by application or domain handlers to record a change made to a configuration item.
/// </remarks>
public class ConfigurationChangelogCreateCommand : Command
{
	/// <summary>
	/// Gets or sets the identifier of the configuration this changelog entry belongs to.
	/// </summary>
	public long ConfigurationId { get; set; }

	/// <summary>
	/// Gets or sets the numeric identifier of the specific configuration item that changed.
	/// </summary>
	public long ItemId { get; set; }

	/// <summary>
	/// Gets or sets the key that was changed.
	/// </summary>
	public string Key { get; set; }

	/// <summary>
	/// Gets or sets the value of the property after the change.
	/// </summary>
	public string Value { get; set; }

	/// <summary>
	/// Gets or sets the type of change performed (for example, "Created", "Updated", "Deleted").
	/// </summary>
	public string ChangeType { get; set; }

	/// <summary>
	/// Gets or sets the identifier of the actor who made the change (user, service, or system).
	/// </summary>
	public string ChangedBy { get; set; }

	/// <summary>
	/// Gets or sets the date and time when the change occurred (UTC).
	/// </summary>
	public DateTime ChangedAt { get; set; }
}