namespace Nerosoft.Starfish.Persistent.Data;

/// <summary>
/// Represents configuration data for a specific environment.
/// </summary>
/// <remarks>
/// Inherits from <see cref="Persistent{TKey}"/> with a long identifier.
/// Contains the environment name and an associated secret (sensitive).
/// </remarks>
public class ConfigurationEnvironmentData : Persistent<long>
{
	/// <summary>
	/// Gets or sets the logical name of the environment (for example: "Development", "Staging", "Production").
	/// </summary>
	public string Name { get; set; }
	/// <summary>
	/// Gets or sets the secret value associated with this environment.
	/// </summary>
	/// <remarks>
	/// This property contains sensitive information and should be handled securely
	/// (for example: encrypted at rest and never written to logs).
	/// </remarks>
	public string Secret { get; set; }
}