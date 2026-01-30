using Nerosoft.Euonia.Repository;

namespace Nerosoft.Starfish.Repository.Entities;

internal class ConfigurationPermission : Entity<long>
{
	public long ConfigurationId { get; set; }

	public long EnvironmentId { get; set; }

	public string UserId { get; set; }

	public bool Read { get; set; }

	public bool Write { get; set; }

	public bool Publish { get; set; }

	public Configuration Configuration { get; set; }
}