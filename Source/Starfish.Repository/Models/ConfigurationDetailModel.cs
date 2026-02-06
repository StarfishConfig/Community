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
	/// Gets or sets the date and time when the entity was last updated.
	/// </summary>
	/// <remarks>This property is typically updated automatically when changes are made to the entity. It is
	/// important for tracking the modification history.</remarks>
	public DateTime UpdatedAt { get; set; }

	/// <summary>
	/// Gets or sets the identifier of the user who last updated the record.
	/// </summary>
	/// <remarks>
	/// This property is typically used for tracking changes and auditing purposes. Ensure that the value
	/// is set appropriately to reflect the user responsible for the last modification.
	/// </remarks>
	public string UpdatedBy { get; set; }

}