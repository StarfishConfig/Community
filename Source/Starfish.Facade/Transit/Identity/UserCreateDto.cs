namespace Nerosoft.Starfish.Facade.Transit;

/// <summary>
/// Defines the user creation data transfer object used for registering new users.
/// </summary>
public class UserCreateDto
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
}