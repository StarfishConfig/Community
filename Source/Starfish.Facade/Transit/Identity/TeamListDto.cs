namespace Nerosoft.Starfish.Facade.Transit;

/// <summary>
/// Data transfer object representing a team entry used in lists or summaries.
/// </summary>
public class TeamListDto : TeamBaseDto
{
	/// <summary>
	/// Gets or sets the unique identifier of the team.
	/// </summary>
	public long Id { get; set; }

	/// <summary>
	/// Gets or sets the current number of members in the team.
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
	/// Gets or sets the timestamp when the team was created.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the timestamp when the team was last updated.
	/// </summary>
	public DateTime UpdatedAt { get; set; }
}