using Nerosoft.Euonia.Repository;

namespace Nerosoft.Starfish.Repository.Entities;

/// <summary>
/// Represents a configuration aggregate in the repository.
/// </summary>
/// <remarks>
/// Holds identifying metadata, audit information and related child collections such as
/// environments, permissions and items. Instances are persisted by the repository layer.
/// </remarks>
internal sealed class Configuration : Entity<long>, IAuditable
{
    /// <summary>
    /// Gets or sets the identifier of the owning team.
    /// </summary>
    public long TeamId { get; set; }

    /// <summary>
    /// Gets or sets the machine-friendly code that uniquely identifies the configuration.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the human-friendly name of the configuration.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets an optional description for the configuration.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp (UTC) for the configuration.
    /// </summary>
    /// <remarks>
    /// Timestamps are intended to be stored and interpreted as UTC.
    /// </remarks>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the last update timestamp (UTC) for the configuration.
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

    /// <summary>
    /// Gets or sets the collection of environment entries related to this configuration.
    /// </summary>
    public HashSet<ConfigurationEnvironment> Environments { get; set; } = [];
}