namespace Nerosoft.Starfish.Domain.Constants;

/// <summary>
/// Represents the types of user password updates.
/// </summary>
public class UserPasswordUpdateType
{
	/// <summary>
	/// Represents a password reset initiated by an administrator.
	/// </summary>
	public const string Reset = "Reset";

	/// <summary>
	/// Represents a password change initiated by the user.
	/// </summary>
	public const string Change = "Change";
}