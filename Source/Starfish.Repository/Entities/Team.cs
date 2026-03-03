namespace Nerosoft.Starfish.Repository.Entities;

internal class Team : AuditableEntity<long>
{
	public string OwnerId { get; set; }

	public string Name { get; set; }

	public int MembersCount { get; set; }
	
	public string Description { get; set; }

	public HashSet<TeamMember> Members { get; set; }
}