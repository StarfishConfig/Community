namespace Nerosoft.Starfish.Transit;

/// <summary>
/// Data transfer object used to update an existing configuration.
/// </summary>
/// <remarks>
/// Provide the identifier of the existing configuration and any fields to update.
/// Validation expectations:
/// - <see cref="Code"/> should remain unique within the owning team.
/// - <see cref="Name"/> is a human-readable display name.
/// - <see cref="Symbol"/> is optional and may be null.
/// </remarks>
public class ConfigurationUpdateDto
{
	/// <summary>
	/// Gets or sets the vanity identifier (code) of the configuration.
	/// </summary>
	/// <remarks>
	/// The code must be unique within the team that owns the configuration and is used for programmatic lookups.
	/// Prefer short, alphanumeric values without whitespace.
	/// </remarks>
	public string Code { get; set; }

	/// <summary>
	/// Gets or sets the human-readable name of the configuration.
	/// </summary>
	/// <value>A display name intended for UI presentation.</value>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets an optional description providing additional details about the configuration.
	/// </summary>
	/// <value>Longer, human-readable information about the configuration. May be null or empty.</value>
	public string Description { get; set; }

	/// <summary>
	/// Gets or sets the separator or symbol used in configuration keys or labels.
	/// </summary>
	/// <value>A single character symbol or null if not provided.</value>
	public char? Symbol { get; set; }
}