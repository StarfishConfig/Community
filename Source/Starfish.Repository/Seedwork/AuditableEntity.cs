using Nerosoft.Euonia.Repository;

namespace Nerosoft.Starfish.Repository;

/// <summary>
/// Provides a base class for entities that require audit tracking, including creation, update, and soft-deletion
/// metadata.
/// </summary>
/// <remarks>This class supplies common audit properties for tracking when and by whom an entity was created,
/// updated, or deleted. Timestamps are intended to be stored and interpreted as UTC. Soft-deletion is supported via the
/// <see cref="IsDeleted"/> and <see cref="DeletedAt"/> properties. Inherit from this class to enable audit
/// functionality in domain entities.</remarks>
/// <typeparam name="TKey">The type of the entity's unique identifier. Must implement <see cref="IEquatable{TKey}"/> to support equality
/// comparisons.</typeparam>
internal abstract class AuditableEntity<TKey> : Entity<TKey>, IAuditable
	where TKey : IEquatable<TKey>
{
	/// <summary>
	/// Gets or sets the creation timestamp (UTC) for the entity.
	/// </summary>
	/// <remarks>
	/// Timestamps are intended to be stored and interpreted as UTC.
	/// </remarks>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the last update timestamp (UTC) for the entity.
	/// </summary>
	/// <remarks>
	/// Timestamps are intended to be stored and interpreted as UTC.
	/// </remarks>
	public DateTime UpdatedAt { get; set; }

	/// <summary>
	/// Gets or sets the deletion timestamp (UTC), if the entity was soft-deleted.
	/// </summary>
	public DateTime? DeletedAt { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether the entity has been soft-deleted.
	/// </summary>
	public bool IsDeleted { get; set; }

	/// <summary>
	/// Gets or sets the identifier of the principal that created the entity.
	/// </summary>
	public string CreatedBy { get; set; }

	/// <summary>
	/// Gets or sets the identifier of the principal that last updated the entity.
	/// </summary>
	public string UpdatedBy { get; set; }

	/// <summary>
	/// Gets or sets the identifier of the principal that deleted the entity.
	/// </summary>
	public string DeletedBy { get; set; }
}
