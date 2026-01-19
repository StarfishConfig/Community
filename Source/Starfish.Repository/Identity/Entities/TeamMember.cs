using Nerosoft.Euonia.Repository;

namespace Nerosoft.Starfish.Repository.Entities;

internal class TeamMember : Entity<long>
{
	public long TeamId { get; set; }

	public string UserId { get; set; }
}