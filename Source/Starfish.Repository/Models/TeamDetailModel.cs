namespace Nerosoft.Starfish.Repository.Models;

/// <summary>
/// Represents the details of a team.
/// </summary>
public class TeamDetailModel
{
	/// <summary>
	/// Gets or sets the identifier of the team.
	/// </summary>
	public long Id { get; set; }

	/// <summary>
	/// Gets or sets the name of the team.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the description of the team.
	/// </summary>
	public string Description { get; set; }

	/// <summary>
	/// Gets or sets the identifier of the owner of the team.
	/// </summary>
	public string OwnerId { get; set; }

	/// <summary>
	/// Gets or sets the username of the owner of the team.
	/// </summary>
	public string OwnerUsername { get; set; }

	/// <summary>
	/// Gets or sets the nickname of the owner of the team.
	/// </summary>
	public string OwnerNickname { get; set; }

	/// <summary>
	/// Gets or sets the number of members in the team.
	/// </summary>
	public int MembersCount { get; set; }

	/// <summary>
	/// Gets or sets the date and time when the team was created.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the date and time when the team was last updated.
	/// </summary>
	public DateTime UpdatedAt { get; set; }
}