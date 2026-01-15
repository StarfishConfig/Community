namespace Nerosoft.Starfish.Facade.Transit;

/// <summary>
/// DTO used to carry data required for resetting a user's password.
/// </summary>
public class UserPasswordResetDto
{
	/// <summary>
	/// The new plaintext password the user intends to use.
	/// Ensure this value is validated and handled securely (hashed/stored safely) by the service.
	/// </summary>
	public string NewPassword { get; set; }

	/// <summary>
	/// Token issued to authorize the password reset operation (commonly sent via email or SMS).
	/// </summary>
	public string ResetToken { get; set; }
}