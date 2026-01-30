namespace Nerosoft.Starfish.Infrastructure;

/// <summary>
/// Specifies the contexts in which a one-time password (OTP) can be used within authentication workflows.
/// </summary>
/// <remarks>Use this enumeration to indicate the intended purpose of an OTP, such as user authentication,
/// registration, password reset, or two-factor authentication. This helps ensure that OTPs are validated and processed
/// according to their specific security context.</remarks>
public enum OnetimePasswordUsage
{
	/// <summary>
	/// Represents a one-time password used for user authentication during sign-in.
	/// </summary>
	Authentication = 1,
	/// <summary>
	/// Represents a one-time password used for user registration.
	/// </summary>
	Registration = 2,
	/// <summary>
	/// Represents a one-time password used for password reset.
	/// </summary>
	ResetPassword = 3,
	/// <summary>
	/// Represents a one-time password used for two-factor authentication.
	/// </summary>
	TwoFactorAuthentication = 4,

	/// <summary>
	/// Represents a one-time password used for binding actions, such as linking a phone number or email address.
	/// </summary>
	Binding = 5,
}
