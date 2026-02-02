namespace Nerosoft.Starfish.Transit;

/// <summary>
/// Represents a data transfer object for creating a configuration item.
/// </summary>
public class ConfigurationItemCreateDto
{
	/// <summary>
	/// Gets or sets the key of the configuration item.
	/// </summary>
	public string Key { get; set; }

	/// <summary>
	/// Gets or sets the configuration values for different environments.
	/// </summary>
	public Dictionary<long, string> Values { get; set; }
}