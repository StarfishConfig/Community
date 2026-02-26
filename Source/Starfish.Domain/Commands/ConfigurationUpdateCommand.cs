using Nerosoft.Euonia.Domain;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Domain.Commands;

/// <summary>
/// Represents a command to update an existing configuration.
/// </summary>
/// <remarks>
/// Carries the values required to update a configuration entity identified by <see cref="Id"/>.
/// Validation rules such as the uniqueness of <see cref="Code"/> within a team are handled by the application layer.
/// </remarks>
public class ConfigurationUpdateCommand : Command
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ConfigurationUpdateCommand"/> class with the specified identifier.
	/// </summary>
	/// <param name="id">The configuration identifier.</param>
	public ConfigurationUpdateCommand(long id)
	{
		Id = id;
	}

	/// <summary>
	/// Gets the identifier of the configuration to be updated.
	/// </summary>
	public long Id { get; }

	/// <summary>
	/// Gets or sets the vanity identifier (code) of the configuration.
	/// </summary>
	/// <remarks>
	/// The code must be unique within the team that owns the configuration.
	/// </remarks>
	public string Code { get; set; }

	/// <summary>
	/// Gets or sets the human-readable name of the configuration.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the secret value associated with the configuration, which may contain sensitive information.
	/// </summary>
	public string Secret { get; set; }
	
	/// <summary>
	/// Gets or sets an optional description providing additional details about the configuration.
	/// </summary>
	public string Description { get; set; }
	
	/// <summary>
	/// Gets or sets a value indicating whether the configuration can be referenced by other configurations within the same team.
	/// </summary>
	public bool Shared  { get; set; }
	
	/// <summary>
	/// Gets or sets a dictionary mapping user IDs to their corresponding permission grant states for this configuration.
	/// </summary>
	public Dictionary<string, ConfigurationPermissionGrantState> Permissions { get; set; }
}