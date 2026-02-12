using Nerosoft.Euonia.Repository;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Entities;

/// <summary>
/// Represents an environment entry for a configuration.
/// </summary>
/// <remarks>
/// Holds environment-specific values (name and secret) and a reference to the parent configuration.
/// </remarks>
internal class ConfigurationEnvironment : Entity<long>
{
	/// <summary>
	/// Gets or sets the identifier of the parent configuration.
	/// </summary>
	public long ConfigurationId { get; set; }

	/// <summary>
	/// Gets or sets the name of the environment (for example "Development", "Staging", "Production").
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the secret associated with this environment.
	/// </summary>
	/// <remarks>
	/// This is sensitive data. Store and transmit securely and avoid logging in plaintext.
	/// </remarks>
	public string Secret { get; set; }

	/// <summary>
	/// Gets or sets the description of the environment.
	/// </summary>
	public string Description { get; set; }

	/// <summary>
	/// Gets or sets the fork identifier if this item is part of a forked configuration.
	/// A null value indicates that the item is not part of any fork.
	/// </summary>
	public long? ForkId { get; set; }

	/// <summary>
	/// Gets or sets the fork tags.
	/// </summary>
	/// <remarks>
	/// <![CDATA[The tags are stored as a url-encoded string in the format: "cluster=cluster1&ip=192.168.1.10&machine=machine1&subnet=192.168.1.1/24". And they are sorted by key in ascending order.]]>
	/// </remarks>
	public string Tags { get; set; }

	/// <summary>
	/// Gets or sets the status of the configuration item.
	/// </summary>
	public ConfigurationStatus Status { get; set; }

	/// <summary>
	/// Navigation property to the parent <see cref="Configuration"/>.
	/// </summary>
	public Configuration Configuration { get; set; }

	/// <summary>
	/// Gets or sets the collection of permissions associated with this configuration.
	/// </summary>
	public HashSet<ConfigurationPermission> Permissions { get; set; } = [];

	/// <summary>
	/// Gets or sets the collection of configuration items contained by this configuration.
	/// </summary>
	public HashSet<ConfigurationItem> Items { get; set; } = [];
}