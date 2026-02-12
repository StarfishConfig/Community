using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Transit;

/// <summary>
///	Represents the data transfer object for a configuration environment, including its identifier, name, secret, and
/// status.
/// </summary>
/// <remarks>This class is typically used to transfer environment configuration data between application layers or
/// services. It encapsulates the essential properties required to identify and describe a configuration
/// environment.</remarks>
public class ConfigurationEnvironmentDto
{
	/// <summary>
	/// Gets or sets the unique identifier for the entity.
	/// </summary>
	public long Id { get; set; }

	/// <summary>
	/// Gets or sets the name associated with the object.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the secret value associated with this instance.
	/// </summary>
	public string Secret { get; set; }

	/// <summary>
	/// Gets or sets the current configuration status.
	/// </summary>
	public ConfigurationStatus Status { get; set; }
}
