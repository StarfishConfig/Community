namespace Nerosoft.Starfish.Transit;

/// <summary>
/// Data transfer object representing detailed information about a team.
/// </summary>
public class TeamDetailDto : TeamBaseDto
{
	/// <summary>
	/// Gets or sets the unique identifier of the team.
	/// </summary>
	public long Id { get; set; }

	/// <summary>
	/// Gets or sets the number of members currently in the team.
	/// </summary>
	public int MembersCount { get; set; }

	/// <summary>
	/// Gets or sets the identifier of the user who owns the team.
	/// </summary>
	public string OwnerId { get; set; }

	/// <summary>
	/// Gets or sets the display name or username of the team owner.
	/// </summary>
	public string OwnerName { get; set; }

	/// <summary>
	/// Gets or sets the date and time when the team was created.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the date and time when the team was last updated.
	/// </summary>
	public DateTime UpdatedAt { get; set; }
}