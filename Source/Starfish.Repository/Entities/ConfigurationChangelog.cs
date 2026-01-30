using Nerosoft.Euonia.Repository;

namespace Nerosoft.Starfish.Repository.Entities;

/// <summary>
/// Represents an audit entry for a configuration change.
/// </summary>
/// <remarks>
/// Each instance records a single change to a configuration item, including who made the change
/// and when. Inherits the identifier from <see cref="Entity{TKey}"/> where the key type is <see cref="long"/>.
/// </remarks>
internal class ConfigurationChangelog : Entity<long>
{
	/// <summary>
	/// Gets or sets the identifier of the configuration this change belongs to.
	/// Typically, an Int64 identifier for a configuration collection or group.
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
	/// Gets or sets the value associated with the configuration key after the change.
	/// </summary>
	public string Value { get; set; }

	/// <summary>
	/// Gets or sets the type of change performed (e.g., "Created", "Updated", "Deleted").
	/// </summary>
	public string ChangeType { get; set; }

	/// <summary>
	/// Gets or sets the identifier (username, id, or system) of who made the change.
	/// </summary>
	public string ChangedBy { get; set; }

	/// <summary>
	/// Gets or sets the UTC date and time when the change occurred.
	/// </summary>
	public DateTime ChangedAt { get; set; }
}