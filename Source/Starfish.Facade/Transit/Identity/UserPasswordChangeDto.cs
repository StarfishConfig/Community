namespace Nerosoft.Starfish.Facade.Transit;

/// <summary>
/// DTO used to carry data required for changing a user's password.
/// </summary>
/// <remarks>
/// The service consuming this DTO must validate the current password and securely handle the new password
/// (perform strength checks, hashing, and avoid logging plaintext values).
/// </remarks>
public class UserPasswordChangeDto
{
	/// <summary>
	/// The user's current plaintext password used for verification.
	/// </summary>
	public string OldPassword { get; set; }

	/// <summary>
	/// The user's new plaintext password to be set.
	/// </summary>
	/// <remarks>
	/// Ensure this value is validated for strength and stored securely (hashed) by the authentication service.
	/// </remarks>
	public string NewPassword { get; set; }
}