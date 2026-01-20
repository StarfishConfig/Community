namespace Nerosoft.Starfish.Repository.Models;

public class TeamListModel
{
	public long Id { get; set; }

	public string Name { get; set; }

	public string Description { get; set; }

	public int MembersCount { get; set; }

	public string OwnerId { get; set; }

	public string OwnerUsername { get; set; }

	public string OwnerNickname { get; set; }

	public DateTime CreatedAt { get; set; }

	public DateTime UpdatedAt { get; set; }
}