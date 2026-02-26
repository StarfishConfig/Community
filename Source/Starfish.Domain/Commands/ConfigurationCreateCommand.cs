using Nerosoft.Euonia.Domain;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Domain.Commands;

/// <summary>
/// Represents a command to create a configuration within a specific team.
/// </summary>
/// <remarks>
/// Instances of this command carry the necessary data to create a new configuration
/// such as its identifying code, display name, description and symbol.
/// This class inherits from <see cref="Command"/> provided by <c>Nerosoft.Euonia.Domain</c>.
/// </remarks>
public class ConfigurationCreateCommand : Command
{
	/// <summary>
	/// Gets or sets the identifier of the team that will own the configuration.
	/// </summary>
	/// <value>The team identifier.</value>
	public long TeamId { get; set; }

	/// <summary>
	/// Gets or sets the unique code used to reference the configuration.
	/// </summary>
	/// <value>A short, unique code string.</value>
	public string Code { get; set; }

	/// <summary>
	/// Gets or sets the human-readable name of the configuration.
	/// </summary>
	/// <value>The display name.</value>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the secret value associated with the configuration, which may contain sensitive information.
	/// </summary>
	public string Secret { get; set; }
	
	/// <summary>
	/// Gets or sets an optional description providing details about the configuration.
	/// </summary>
	/// <value>A descriptive text.</value>
	public string Description { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether the configuration can be referenced by other configurations within the same team.
	/// </summary>
	public bool Shared { get; set; }

	/// <summary>
	/// Gets or sets a dictionary mapping user IDs to their corresponding permission grant states for this configuration.
	/// </summary>
	public Dictionary<string, ConfigurationPermissionGrantState> Permissions { get; set; }
}