namespace Nerosoft.Starfish.Shared;

/// <summary>
/// Contains constants for length constraints of various properties used across the application.
/// </summary>
public static class LengthConstraints
{
	/// <summary>
	/// Maximum length for user ID
	/// </summary>
	public const int UserIdMaximumLength = 20;

	/// <summary>
	/// Username length constraints(minimum).
	/// </summary>
	public const int UsernameMinimumLength = 4;

	/// <summary>
	/// Username length constraints(maximum).
	/// </summary>
	public const int UsernameMaximumLength = 255;

	/// <summary>
	/// Email length constraints(maximum).
	/// </summary>
	public const int EmailMaximumLength = 255;

	/// <summary>
	/// Phone number length constraints(maximum).
	/// </summary>
	public const int PhoneMaximumLength = 20;

	/// <summary>
	/// Password length constraints(minimum).
	/// </summary>
	public const int PasswordMinimumLength = 6;

	/// <summary>
	/// Password length constraints(maximum).
	/// </summary>
	public const int PasswordMaximumLength = 32;

	/// <summary>
	/// Nickname length constraints(maximum).
	/// </summary>
	public const int NicknameMaximumLength = 50;

	/// <summary>
	/// Description length constraints(maximum).
	/// </summary>
	public const int DescriptionMaximumLength = 2000;

	/// <summary>
	/// Remark length constraints(maximum).
	/// </summary>
	public const int RemarkMaximumLength = 500;

	/// <summary>
	/// Team name length constraints(maximum).
	/// </summary>
	public const int TeamNameMaximumLength = 50;

	/// <summary>
	/// Request ID length constraints(maximum).
	/// </summary>
	public const int RequestIdLength = 36;

	/// <summary>
	/// IP address length constraints(maximum).
	/// </summary>
	public const int IpAddressMaximumLength = 15;

	/// <summary>
	/// Configuration code length constraints(maximum).
	/// </summary>
	public const int ConfigurationCodeMaximumLength = 100;

	/// <summary>
	/// Configuration code length constraints(minimum).
	/// </summary>
	public const int ConfigurationCodeMinimumLength = 4;

	/// <summary>
	/// Configuration name length constraints(maximum).
	/// </summary>
	public const int ConfigurationNameMaximumLength = 200;

	/// <summary>
	/// Configuration name length constraints(minimum).
	/// </summary>
	public const int ConfigurationNameMinimumLength = 4;
	
	/// <summary>
	/// Configuration description length constraints(maximum).
	/// </summary>
	public const int ConfigurationKeyMaximumLength = 100;
	
	/// <summary>
	/// Configuration value length constraints(maximum).
	/// </summary>
	public const int ConfigurationValueMaximumLength = 2048;
	
	/// <summary>
	/// Configuration tags length constraints(maximum).
	/// </summary>
	public const int ConfigurationTagsMaximumLength = 500;
}