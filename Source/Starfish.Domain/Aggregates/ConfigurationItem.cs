using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Domain.Aggregates;

/// <summary>
/// Represents a configuration item.
/// </summary>
internal class ConfigurationItem : Entity<long>
{
	/// <summary>
	/// Gets the key of the configuration item.
	/// </summary>
	public string Key { get; private set; }

	/// <summary>
	/// Gets the environment name that the configuration value applies to.
	/// </summary>
	/// <remarks>
	///	The environment can be a specific environment name or a wildcard character '*'
	///	to indicate that the configuration value applies to all environments.
	/// </remarks>
	public string Environment { get; private set; }
	
	/// <summary>
	/// Gets the value of the configuration item.
	/// </summary>
	public string Value { get; private set; }
}