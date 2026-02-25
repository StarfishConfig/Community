using Nerosoft.Euonia.Repository;

namespace Nerosoft.Starfish.Repository.Entities;

/// <summary>
/// Represents a reference from one configuration to another within the same team.
/// </summary>
internal class ConfigurationReference : Entity<long>
{
	/// <summary>
	/// Gets or sets the identifier of the owning configuration that holds this reference.
	/// </summary>
	public long ConfigurationId { get; set; }

	/// <summary>
	/// Gets or sets the identifier of the referenced configuration that is being pointed to.
	/// </summary>
	public long ReferenceId { get; set; }

	/// <summary>
	/// Navigation property to the associated <see cref="Configuration"/>.
	/// </summary>
	public Configuration Configuration { get; set; }
}