using Nerosoft.Euonia.Repository;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Entities;

internal class ConfigurationItem : Entity<long>, IAuditable
{
	public long ConfigurationId { get; set; }

	public long EnvironmentId { get; set; }

	public string Key { get; set; }

	public string Value { get; set; }

	public ConfigurationStatus Status { get; set; }

	public DateTime CreatedAt { get; set; }

	public DateTime UpdatedAt { get; set; }

	public DateTime? DeletedAt { get; set; }

	public bool IsDeleted { get; set; }

	public string CreatedBy { get; set; }

	public string UpdatedBy { get; set; }

	public string DeletedBy { get; set; }

	public Configuration Configuration { get; set; }

	public HashSet<ConfigurationFork> Forks { get; set; } = [];
}