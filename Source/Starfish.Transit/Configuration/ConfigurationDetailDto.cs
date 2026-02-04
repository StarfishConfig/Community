namespace Nerosoft.Starfish.Transit;

/// <summary>
/// Data transfer object containing detailed information about a configuration,
/// including identifying metadata and the list of associated environment names.
/// </summary>
public class ConfigurationDetailDto
{
	/// <summary>
	/// Gets or sets the unique identifier of the configuration.
	/// </summary>
	public long Id { get; set; }

	/// <summary>
	/// Gets or sets the vanity code that uniquely identifies the configuration.
	/// </summary>
	public string Code { get; set; }

	/// <summary>
	/// Gets or sets the human-readable name of the configuration.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the identifier of the team that owns the configuration.
	/// </summary>
	public long TeamId { get; set; }

	/// <summary>
	/// Gets or sets the display name of the owning team.
	/// </summary>
	public string TeamName { get; set; }

	/// <summary>
	/// Gets or sets an optional description providing additional details about the configuration.
	/// </summary>
	public string Description { get; set; }

	/// <summary>
	/// Gets or sets the collection of environment names associated with this configuration.
	/// </summary>
	/// <value>
	/// A list of environment identifiers or names. Initialized to an empty list.
	/// </value>
	public List<string> Environments { get; set; }
}