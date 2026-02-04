using Nerosoft.Euonia.Repository;

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
	/// Navigation property to the parent <see cref="Configuration"/>.
	/// </summary>
	public Configuration Configuration { get; set; }
}