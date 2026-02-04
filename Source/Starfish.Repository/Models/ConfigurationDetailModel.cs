namespace Nerosoft.Starfish.Repository.Models;

/// <summary>
/// Represents detailed information about a configuration, including its
/// identifying metadata and associated environment entries.
/// </summary>
public class ConfigurationDetailModel
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
	/// Gets or sets an optional description providing additional details about the configuration.
	/// </summary>
	public string Description { get; set; }

	/// <summary>
	/// Gets or sets the collection of environment-specific configuration models associated with this configuration.
	/// </summary>
	/// <value>
	/// A list of <see cref="ConfigurationEnvironmentModel"/> instances. Initialized to an empty list.
	/// </value>
	public IList<ConfigurationEnvironmentModel> Environments { get; set; } = new System.Collections.Generic.List<ConfigurationEnvironmentModel>();
}