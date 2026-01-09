namespace Nerosoft.Starfish.Shared;

/// <summary>
/// Provides regex patterns for various validations.
/// </summary>
public static class RegexPattern
{
	/// <summary>
	/// Regex pattern for validating usernames, which can be:
	/// - 3 to 20 characters long
	/// - Contain alphanumeric characters, underscores, or dots
	/// - A valid phone number starting with 1 and followed by 3-9 and 9 digits
	/// - A valid email address
	/// </summary>
	public const string Username = @"^(?:(?=.{3,20}$)[a-zA-Z0-9](?:[a-zA-Z0-9_.]*[a-zA-Z0-9_])?|1[3-9]\d{9}|[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,})$";
}
