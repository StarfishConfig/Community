namespace Nerosoft.Starfish.Repository.Models;

/// <summary>
/// Represents an environment-specific configuration model used by the repository layer.
/// </summary>
/// <remarks>
/// Contains an identifier, a human-readable name and an environment secret. The secret
/// should be handled securely (for example: encrypted at rest and not written to logs).
/// </remarks>
public class ConfigurationEnvironmentModel
{
	/// <summary>
	/// Gets or sets the unique identifier for the environment configuration.
	/// </summary>
	public long Id { get; set; }

	/// <summary>
	/// Gets or sets the logical name of the environment (for example: "Development", "Staging", "Production").
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the secret or credential associated with this environment.
	/// </summary>
	/// <remarks>
	/// This property contains sensitive information and must be protected (encrypted at rest,
	/// transmitted securely, and not logged in plain text).
	/// </remarks>
	public string Secret { get; set; }
}