namespace Nerosoft.Starfish.Shared;

/// <summary>
/// Contains constants for validation rules used in the system.
/// </summary>
public static class ValidationConstants
{
	/// <summary>
	/// Username length constraints(minimum).
	/// </summary>
	public const int UsernameMinimumLength = 4;

	/// <summary>
	/// Username length constraints(maximum).
	/// </summary>
	public const int UsernameMaximumLength = 50;

	/// <summary>
	/// Email length constraints(maximum).
	/// </summary>
	public const int EmailMaximumLength = 255;

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
}