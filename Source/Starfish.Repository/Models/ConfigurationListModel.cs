namespace Nerosoft.Starfish.Repository.Models;

/// <summary>
/// Represents a summarized view of a configuration entity used for list displays or search results.
/// </summary>
/// <remarks>
/// This model is a lightweight DTO intended for listing scenarios — it contains identifying metadata
/// and a timestamp indicating when the configuration was last modified. Avoid placing sensitive data here.
/// </remarks>
public class ConfigurationListModel
{
	/// <summary>
	/// Gets or sets the unique identifier of the configuration.
	/// </summary>
	/// <value>The primary key identifier.</value>
	public long Id { get; set; }

	/// <summary>
	/// Gets or sets the machine-friendly code or key for the configuration.
	/// </summary>
	/// <value>A short, unique string used to reference the configuration programmatically.</value>
	public string Code { get; set; }

	/// <summary>
	/// Gets or sets the human-readable name of the configuration.
	/// </summary>
	/// <value>A friendly display name for UI lists.</value>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the identifier of the team that owns the configuration.
	/// </summary>
	/// <value>The team primary key.</value>
	public long TeamId { get; set; }

	/// <summary>
	/// Gets or sets a short description explaining the purpose or contents of the configuration.
	/// </summary>
	/// <value>An optional, brief description.</value>
	public string Description { get; set; }

	/// <summary>
	/// Gets or sets the timestamp of the last update performed on this configuration.
	/// </summary>
	/// <remarks>
	/// This timestamp should be treated as UTC. Use it to order or filter list results by recency.
	/// </remarks>
	/// <value>The UTC date and time when the configuration was last modified.</value>
	public DateTime UpdateTime { get; set; }
}