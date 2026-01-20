namespace Nerosoft.Starfish.Repository.Models;

public class TeamMemberModel
{
	public long Id { get; set; }

	public long TeamId { get; set; }

	public string UserId { get; set; }

	public string Username { get; set; }

	public string Nickname { get; set; }

	public string Email { get; set; }

	public string Phone { get; set; }

	public DateTime JoinedAt { get; set; }
}