namespace Nerosoft.Starfish.Persistent;

/// <summary>
/// Defines the data structure for user information.
/// </summary>
/// <remarks>
///	The UserData class encapsulates various properties related to a user, including
/// credentials, contact information, account status, and assigned roles.
/// </remarks>
internal class UserData : Persistent<string>
{
	public UserData()
	{ }

	public UserData(string id)
		: base(id)
	{ }

	public string Username { get; set; }

	public string Nickname { get; set; }

	public string Password { get; set; }

	public string Email { get; set; }

	public string Phone { get; set; }

	public int AccessFailedCount { get; set; }

	public DateTime? PasswordChangedAt { get; set; }

	public DateTime? LockoutEnd { get; set; }

	public HashSet<string> Roles { get; set; }
}