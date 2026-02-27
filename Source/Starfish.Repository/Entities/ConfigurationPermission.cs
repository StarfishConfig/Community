using Nerosoft.Euonia.Repository;

namespace Nerosoft.Starfish.Repository.Entities;

/// <summary>
/// Represents a permission entry that grants a user specific operations (read, write, publish)
/// for a configuration within a given environment.
/// </summary>
/// <remarks>
/// This entity is part of the repository layer and is used to persist authorization rules.
/// It associates a user with a configuration and environment, specifying the allowed operations.
/// </remarks>
internal class ConfigurationPermission : Entity<long>
{
	/// <summary>
	/// Gets or sets the identifier of the associated configuration.
	/// </summary>
	public long ConfigurationId { get; set; }

	/// <summary>
	/// Gets or sets the identifier of the user to whom the permissions are granted.
	/// </summary>
	public string UserId { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether the user has the permission to view a configuration or not.
	/// </summary>
	public bool Read { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether the user has the permission to modify a configuration or not.
	/// </summary>
	public bool Write { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether the user has the permission to publish a configuration or not.
	/// </summary>
	public bool Publish { get; set; }

	/// <summary>
	/// Navigation property to the associated <see cref="Configuration"/>.
	/// </summary>
	public Configuration Configuration { get; set; }
}