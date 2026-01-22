namespace Nerosoft.Starfish.Transit;

/// <summary>
/// Data transfer object representing a team member's public profile information
/// used by the facade/transport layer.
/// </summary>
public class TeamMemberDto
{
	/// <summary>
	/// Gets or sets the unique identifier of the user.
	/// </summary>
	public string UserId { get; set; }

	/// <summary>
	/// Gets or sets the user's username (login name or handle).
	/// </summary>
	public string Username { get; set; }

	/// <summary>
	/// Gets or sets the user's display name or nickname.
	/// </summary>
	public string Nickname { get; set; }

	/// <summary>
	/// Gets or sets the user's primary email address.
	/// </summary>
	public string Email { get; set; }

	/// <summary>
	/// Gets or sets the user's phone number.
	/// </summary>
	public string Phone { get; set; }

	/// <summary>
	/// Gets or sets the date and time when the user joined the team.
	/// </summary>
	public DateTime JoinedAt { get; set; }
}