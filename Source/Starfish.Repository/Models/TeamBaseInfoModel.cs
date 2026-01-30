namespace Nerosoft.Starfish.Repository.Models;

/// <summary>
/// Represents the base information for a team.
/// </summary>
public class TeamBaseInfoModel
{
	/// <summary>
	/// Gets or sets the unique identifier for the team.
	/// </summary>
	/// <value>The team identifier.</value>
	public long Id { get; set; }

	/// <summary>
	/// Gets or sets the display name of the team.
	/// </summary>
	/// <value>The team name.</value>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the identifier of the team's owner (user id).
	/// </summary>
	/// <value>The owner's user identifier.</value>
	public string OwnerId { get; set; }

	/// <summary>
	/// Gets or sets the set of member user IDs belonging to the team.
	/// </summary>
	public HashSet<string> Members { get; set; }
}