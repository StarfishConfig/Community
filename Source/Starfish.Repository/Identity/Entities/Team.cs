using Nerosoft.Euonia.Repository;

namespace Nerosoft.Starfish.Repository.Entities;

internal class Team : Entity<long>, IAuditable
{
	public string OwnerId { get; set; }

	public string Name { get; set; }

	public string MemberCount { get; set; }
	
	public string Description { get; set; }

	public DateTime CreatedAt { get; set; }

	public DateTime UpdatedAt { get; set; }

	public DateTime? DeletedAt { get; set; }

	public bool IsDeleted { get; set; }

	public string CreatedBy { get; set; }

	public string UpdatedBy { get; set; }

	public string DeletedBy { get; set; }

	public HashSet<TeamMember> Members { get; set; }
}