namespace Nerosoft.Starfish.Infrastructure;

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

	/// <summary>
	/// Regex pattern for validating phone numbers, which can be:
	/// - Optional leading plus sign
	/// - Optional country code of 1 to 3 digits
	/// - Optional separators such as spaces, dots, or hyphens
	/// - Optional area code enclosed in parentheses
	/// - Three digits followed by a separator and four digits
	/// </summary>
	public const string PhoneNumber = @"^[+]?(\d{1,3})?[\s.-]?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$";

	/// <summary>
	/// Regular expression for matching an email address.
	/// </summary>
	/// <remarks>General Email Regex (RFC 5322 Official Standard) from https://emailregex.com.</remarks>
	public const string EmailAddress = "(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])";

	/// <summary>
	/// Regex pattern for validating configuration codes, which can be:
	/// - 3 to 50 characters long
	/// - Contain alphanumeric characters, dots, underscores, or hyphens
	/// </summary>
	public const string ConfigurationCode = @"^[a-zA-Z0-9._\-]{3,50}$";
}