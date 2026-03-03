using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Persistent.Data;

/// <summary>
/// Represents the persistent data structure for a configuration entity.
/// </summary>
public class ConfigurationData : Persistent<long>
{
	/// <summary>
	/// Initializes a new instance of the ConfigurationData class.
	/// </summary>
	public ConfigurationData()
	{
	}

	/// <summary>
	/// Initializes a new instance of the ConfigurationData class with the specified identifier.
	/// </summary>
	/// <param name="id">The unique identifier for the configuration data. Must be a valid long value.</param>
	public ConfigurationData(long id)
		: base(id)
	{
	}

	/// <summary>
	/// Gets or sets the team identifier associated with the configuration.
	/// </summary>
	/// <value>
	/// A <see cref="long"/> representing the unique identifier of the team.
	/// </value>
	public long TeamId { get; set; }

	/// <summary>
	/// Gets or sets the vanity identifier of the configuration.
	/// </summary>
	/// <value>
	/// A <see cref="string"/> representing the unique code for the configuration.
	/// </value>
	public string Code { get; set; }

	/// <summary>
	/// Gets or sets the name of the configuration.
	/// </summary>
	/// <value>
	/// A <see cref="string"/> representing the display name of the configuration.
	/// </value>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the secret associated with this configuration.
	/// </summary>
	/// <remarks>
	/// This is sensitive data. Store and transmit securely and avoid logging in plaintext.
	/// </remarks>
	public string Secret { get; set; }

	/// <summary>
	/// Gets or sets the description of the configuration.
	/// </summary>
	/// <value>
	/// A <see cref="string"/> providing additional details about the configuration.
	/// </value>
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
	/// Gets or sets the collection of configuration items associated with this configuration.
	/// </summary>
	public Dictionary<string, string> Items { get; set; }

	/// <summary>
	/// Gets or sets the collection of permissions associated with this configuration.
	/// </summary>
	public Dictionary<string, ConfigurationPermissionGrantState> Permissions { get; set; }
}