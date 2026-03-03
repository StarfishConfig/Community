namespace Nerosoft.Starfish.Repository.Entities;

internal class ConfigurationItem : AuditableEntity<long>
{
	/// <summary>
	/// Gets or sets the identifier of the configuration this item belongs to.
	/// Typically, an Int64 identifier for a configuration collection or group.
	/// </summary>
	public long ConfigurationId { get; set; }

	/// <summary>
	/// Gets or sets the configuration key.
	/// </summary>
	public string Key { get; set; }

	/// <summary>
	/// Gets or sets the configuration value.
	/// </summary>
	public string Value { get; set; }

	/// <summary>
	/// Gets or sets the associated Configuration entity.
	/// </summary>
	public Configuration Configuration { get; set; }
}