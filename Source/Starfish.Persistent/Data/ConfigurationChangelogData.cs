namespace Nerosoft.Starfish.Persistent.Data;

/// <summary>
/// Represents a changelog entry for configuration changes.
/// </summary>
internal class ConfigurationChangelogData : Persistent<long>
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