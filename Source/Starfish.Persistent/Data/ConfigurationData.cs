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
	/// Gets or sets the description of the configuration.
	/// </summary>
	/// <value>
	/// A <see cref="string"/> providing additional details about the configuration.
	/// </value>
	public string Description { get; set; }
}