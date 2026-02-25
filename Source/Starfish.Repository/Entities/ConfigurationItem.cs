using Nerosoft.Euonia.Repository;

namespace Nerosoft.Starfish.Repository.Entities;

internal class ConfigurationItem : Entity<long>, IAuditable
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
	/// Gets or sets the UTC date and time when the configuration item was created.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the UTC date and time when the configuration item was last updated.
	/// </summary>
	public DateTime UpdatedAt { get; set; }

	/// <summary>
	/// Gets or sets the UTC date and time when the configuration item was deleted.
	/// </summary>
	public DateTime? DeletedAt { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether the configuration item is deleted.
	/// </summary>
	public bool IsDeleted { get; set; }

	/// <summary>
	/// Gets or sets the identifier (username, id, or system) of who created the configuration item.
	/// </summary>
	public string CreatedBy { get; set; }

	/// <summary>
	/// Gets or sets the identifier (username, id, or system) of who last updated the configuration item.
	/// </summary>
	public string UpdatedBy { get; set; }

	/// <summary>
	/// Gets or sets the identifier (username, id, or system) of who deleted the configuration item.
	/// </summary>
	public string DeletedBy { get; set; }

	/// <summary>
	/// Gets or sets the associated Configuration entity.
	/// </summary>
	public Configuration Configuration { get; set; }
}