namespace Nerosoft.Starfish.Domain.Identity.Data;

/// <summary>
/// Defines the data structure for user information.
/// </summary>
/// <remarks>
///	The UserData class encapsulates various properties related to a user, including
/// credentials, contact information, account status, and assigned roles.
/// </remarks>
public class UserData
{
	public string Username { get; set; }

	public string Nickname { get; set; }

	public string Password { get; set; }

	public string Email { get; set; }

	public string Phone { get; set; }

	public int AccessFailedCount { get; set; }

	public DateTime? PasswordChangedTime { get; set; }

	public DateTime? LockoutEnd { get; set; }

	public HashSet<string> Roles { get; set; }
}