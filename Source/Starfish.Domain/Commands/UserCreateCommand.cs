using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Domain.Commands;

/// <summary>
/// Represents a request to create a new user within the domain.
/// </summary>
public class UserCreateCommand : Command
{
	/// <summary>
	/// The unique username for the user. This is typically used for login.
	/// </summary>
	public string Username { get; set; }

	/// <summary>
	/// The user's password. Handlers should ensure this is stored securely (e.g., hashed) and validated according to policy.
	/// </summary>
	public string Password { get; set; }

	/// <summary>
	/// The user's email address. May be used for verification or communication.
	/// </summary>
	public string Email { get; set; }

	/// <summary>
	/// The user's phone number. May be used for multi-factor authentication or notifications.
	/// </summary>
	public string Phone { get; set; }

	/// <summary>
	/// A display name or nickname for the user.
	/// </summary>
	public string Nickname { get; set; }

	/// <summary>
	/// A collection of role identifiers or names assigned to the user.
	/// </summary>
	public List<string> Roles { get; set; }
}