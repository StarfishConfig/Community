namespace Nerosoft.Starfish.Toolkit;

/// <summary>
/// Specifies the complexity requirements for generated passwords.
/// </summary>
[Flags]
public enum PasswordComplexity
{
	/// <summary>
	/// No specific complexity requirements.
	/// </summary>
	None = 0,

	/// <summary>
	/// Should contain at least one lowercase letter (a-z).
	/// </summary>
	ContainsLowercase = 1,

	/// <summary>
	/// Should contain at least one uppercase letter (A-Z).
	/// </summary>
	ContainsUppercase = 2,

	/// <summary>
	/// Should contain at least one digit (0-9).
	/// </summary>
	ContainsDigit = 4,

	/// <summary>
	/// Should contain at least one symbol (e.g., !@#$%^&*).
	/// </summary>
	ContainsSymbol = 8,

	/// <summary>
	/// Should contain at least one lowercase letter, one uppercase letter, one digit, and one symbol.
	/// </summary>
	All = ContainsLowercase | ContainsUppercase | ContainsDigit | ContainsSymbol
}