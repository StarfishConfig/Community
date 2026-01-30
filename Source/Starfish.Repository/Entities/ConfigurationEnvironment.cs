using Nerosoft.Euonia.Repository;

namespace Nerosoft.Starfish.Repository.Entities;

internal class ConfigurationEnvironment : Entity<long>
{
	public long ConfigurationId { get; set; }

	public string Name { get; set; }

	public string Secret { get; set; }

	public Configuration Configuration { get; set; }
}