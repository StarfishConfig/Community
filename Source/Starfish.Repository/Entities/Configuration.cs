using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Entities;

/// <summary>
/// Represents a configuration aggregate in the repository.
/// </summary>
/// <remarks>
/// Holds identifying metadata, audit information and related child collections such as
/// environments, permissions and items. Instances are persisted by the repository layer.
/// </remarks>
internal sealed class Configuration : AuditableEntity<long>
{
	/// <summary>
	/// Gets or sets the identifier of the owning team.
	/// </summary>
	public long TeamId { get; set; }

	/// <summary>
	/// Gets or sets the optional identifier of the forked configuration, if this configuration was created by forking another configuration.
	/// </summary>
	public long? ForkId { get; set; }

	/// <summary>
	/// Gets or sets the optional tags associated with this configuration, stored as a comma-separated string.
	/// </summary>
	public string Tags { get; set; }

	/// <summary>
	/// Gets or sets the machine-friendly code that uniquely identifies the configuration.
	/// </summary>
	public string Code { get; set; }

	/// <summary>
	/// Gets or sets the human-friendly name of the configuration.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the secret associated with this configuration.
	/// </summary>
	/// <remarks>
	/// This is sensitive data. Store and transmit securely and avoid logging in plaintext.
	/// </remarks>
	public string Secret { get; set; }

	/// <summary>
	/// Gets or sets an optional description for the configuration.
	/// </summary>
	public string Description { get; set; }

	/// <summary>
	/// Gets or sets the status of the configuration item.
	/// </summary>
	public ConfigurationStatus Status { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether the configuration can be referenced by other configurations within the same team.
	/// </summary>
	public bool Shared { get; set; }

	/// <summary>
	/// Gets or sets the collection of permissions associated with this configuration.
	/// </summary>
	public HashSet<ConfigurationPermission> Permissions { get; set; } = [];

	/// <summary>
	/// Gets or sets the collection of configuration items contained by this configuration.
	/// </summary>
	public HashSet<ConfigurationItem> Items { get; set; } = [];

	/// <summary>
	/// Gets or sets the collection of references to other configurations that this configuration depends on.
	/// </summary>
	public HashSet<ConfigurationReference> References { get; set; } = [];
}