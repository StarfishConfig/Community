using Nerosoft.Euonia.Repository;

namespace Nerosoft.Starfish.Repository.Entities;

internal sealed class Configuration : Entity<long>, IAuditable
{
	public long TeamId { get; set; }

	public string Code { get; set; }

	public string Name { get; set; }

	public string Description { get; set; }

	public DateTime CreatedAt { get; set; }

	public DateTime UpdatedAt { get; set; }

	public DateTime? DeletedAt { get; set; }

	public bool IsDeleted { get; set; }

	public string CreatedBy { get; set; }

	public string UpdatedBy { get; set; }

	public string DeletedBy { get; set; }

	public HashSet<ConfigurationEnvironment> Environments { get; set; } = [];

	public HashSet<ConfigurationPermission> Permissions { get; set; } = [];

	public HashSet<ConfigurationItem> Items { get; set; } = [];
}