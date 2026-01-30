using Nerosoft.Euonia.Repository;

namespace Nerosoft.Starfish.Repository.Entities;

/// <summary>
/// Represents a configuration fork entity.
/// </summary>
internal class ConfigurationFork : Entity<long>, IAuditable
{
	/// <summary>
	/// Gets or sets the configuration identifier.
	/// </summary>
	public long ConfigurationId { get; set; }

	/// <summary>
	/// Gets or sets the configuration item identifier.
	/// </summary>
	public long ItemId { get; set; }

	/// <summary>
	/// Gets or sets the fork tags.
	/// </summary>
	/// <remarks>
	/// <![CDATA[The tags are stored as a url-encoded string in the format: "cluster=cluster1&ip=192.168.1.10&machine=machine1&subnet=192.168.1.1/24". And they are sorted by key in ascending order.]]>
	/// </remarks>
	public string Tags { get; set; }

	/// <summary>
	/// Gets or sets the value.
	/// </summary>
	public string Value { get; set; }

	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public DateTime? DeletedAt { get; set; }
	public bool IsDeleted { get; set; }
	public string CreatedBy { get; set; }
	public string UpdatedBy { get; set; }
	public string DeletedBy { get; set; }
}