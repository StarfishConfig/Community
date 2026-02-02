namespace Nerosoft.Starfish.Transit;

/// <summary>
/// Data transfer object representing a configuration entry in a list view.
/// </summary>
public class ConfigurationListDto
{
	/// <summary>
	/// The unique identifier of the configuration.
	/// </summary>
	public long Id { get; set; }

	/// <summary>
	/// The machine-friendly code or key for the configuration.
	/// </summary>
	public string Code { get; set; }

	/// <summary>
	/// The human-readable name of the configuration.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Identifier of the team that owns the configuration.
	/// </summary>
	public long TeamId { get; set; }

	/// <summary>
	/// The display name of the team that owns the configuration.
	/// </summary>
	public string TeamName { get; set; }

	/// <summary>
	/// A short description explaining the purpose or contents of the configuration.
	/// </summary>
	public string Description { get; set; }

	/// <summary>
	/// Timestamp of the last update performed on this configuration.
	/// </summary>
	public DateTime UpdateTime { get; set; }
}