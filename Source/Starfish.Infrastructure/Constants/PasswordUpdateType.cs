namespace Nerosoft.Starfish.Infrastructure;

/// <summary>
/// Represents the types of user password updates.
/// </summary>
public class PasswordUpdateType
{
	/// <summary>
	/// Represents a password reset initiated by an administrator.
	/// </summary>
	public const string Reset = "reset";

	/// <summary>
	/// Represents a password change initiated by the user.
	/// </summary>
	public const string Change = "change";

	/// <summary>
	/// Represents a password creation when a new user is registered.
	/// </summary>
	public const string Create = "create";
}