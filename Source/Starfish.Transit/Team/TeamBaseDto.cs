namespace Nerosoft.Starfish.Transit;

/// <summary>
/// Base data transfer object containing common team fields shared by
/// various team-related DTOs (e.g. create, edit, list).
/// </summary>
public abstract class TeamBaseDto
{
	/// <summary>
	/// Gets or sets the team's name.
	/// </summary>
	/// <remarks>
	/// Should be a concise, human-readable identifier for the team.
	/// </remarks>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the team's description or purpose.
	/// </summary>
	/// <remarks>
	/// Optional longer text describing the team's responsibilities or goals.
	/// </remarks>
	public string Description { get; set; }
}