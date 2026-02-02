using Nerosoft.Euonia.Domain;

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
	/// Gets or sets the identifier of the configuration to be updated.
	/// </summary>
	public long Id { get; set; }

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
	/// Gets or sets an optional description providing additional details about the configuration.
	/// </summary>
	public string Description { get; set; }
}