namespace Nerosoft.Starfish.Transit;

/// <summary>
/// Represents the data required to create a new configuration.
/// </summary>
/// <remarks>
/// This DTO is used when creating a configuration resource via API or service layer.
/// Properties marked as required should be provided by the caller. Validation rules:
/// - <see cref="TeamId"/> must be a positive identifier (> 0).
/// - <see cref="Code"/> should be a short, unique, non-empty string (recommended max length: 64).
/// - <see cref="Name"/> should be a human-readable non-empty name (recommended max length: 256).
/// - <see cref="Description"/> is optional and may be null or empty.
/// </remarks>
/// <example>
/// Example usage:
/// <code>
/// var dto = new ConfigurationCreateDto
/// {
///     TeamId = 123,
///     Code = "CFG-01",
///     Name = "Default Configuration",
///     Description = "Configuration used for default deployments.",
/// };
/// </code>
/// </example>
public class ConfigurationCreateDto
{
	/// <summary>
	/// Gets or sets the identifier of the team that will own the configuration.
	/// </summary>
	/// <value>The team identifier. Must be greater than zero.</value>
	/// <remarks>
	/// This value associates the created configuration with a specific team or tenant.
	/// Ensure the caller has permission to create configurations for this team.
	/// </remarks>
	/// <example>123</example>
	public long TeamId { get; set; }

	/// <summary>
	/// Gets or sets the unique code used to reference the configuration.
	/// </summary>
	/// <value>A short, unique code string. Should be non-empty and unique within the team scope.</value>
	/// <remarks>
	/// The code is intended for programmatic lookups and should avoid whitespace.
	/// Consider enforcing a pattern (e.g., alphanumeric and dashes) at the API boundary.
	/// </remarks>
	/// <example>CFG-01</example>
	public string Code { get; set; }

	/// <summary>
	/// Gets or sets the human-readable name of the configuration.
	/// </summary>
	/// <value>The display name. Intended for UI presentation.</value>
	/// <remarks>
	/// This value should be meaningful to end users. It may contain spaces and punctuation.
	/// </remarks>
	/// <example>Default Configuration</example>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets an optional description providing details about the configuration.
	/// </summary>
	/// <value>A descriptive text. Can be null or empty.</value>
	/// <remarks>
	/// Use this field to store longer, human-readable information about the purpose and
	/// usage of the configuration. Not required for creation.
	/// </remarks>
	/// <example>Configuration used for default deployments and automated tests.</example>
	public string Description { get; set; }
}