namespace Nerosoft.Starfish.Infrastructure;

/// <summary>
/// Contains constants related to one-time passwords.
/// </summary>
public class OnetimePasswordConstants
{
	/// <summary>
	/// Represents the name of the database table used for storing one-time passwords.
	/// </summary>
	public const string TableName = "onetime_password";

	/// <summary>
	/// Represents the name of the column used to store the request ID in a database.
	/// </summary>
	public const string RequestIdColumnName = "request_id";

	/// <summary>
	/// Represents the name of the database index used to optimize one-time password request lookups.
	/// </summary>
	public const string RequestIndexName = "onetime_password_idx_request_id";

	/// <summary>
	/// Represents the required length, in characters, of a request identifier.
	/// </summary>
	/// <remarks>Use this constant to validate or enforce the standard length of request identifiers throughout the
	/// application. Ensuring a consistent identifier length can help maintain interoperability and simplify validation
	/// logic.</remarks>
	public const int RequestIdLength = 36;

	/// <summary>
	/// Represents the name of the column used to store code values.
	/// </summary>
	public const string CodeColumnName = "code";

	/// <summary>
	/// Represents the length, in characters, of the one-time password code.
	/// </summary>
	public const int CodeLength = 6;

	/// <summary>
	/// Represents the name of the database column used to store recipient information.
	/// </summary>
	public const string RecipientColumnName = "recipient";

	/// <summary>
	/// Gets the maximum length allowed for recipient names.
	/// </summary>
	/// <remarks>This constant defines the upper limit for the length of recipient names in character count.
	/// Exceeding this limit may result in validation errors.</remarks>
	public const int RecipientMaxLength = 255;

	/// <summary>
	/// Represents the name of the column used to store expiration information.
	/// </summary>
	public const string ExpirationColumnName = "expiration";

	/// <summary>
	/// Represents the name of the column that indicates whether an item is checked.
	/// </summary>
	public const string CheckedColumnName = "checked";

	/// <summary>
	/// Represents the name of the column used to store duration values.
	/// </summary>
	public const string DurationColumnName = "duration";

	/// <summary>
	/// Represents the name of the column used for tracking usage data.
	/// </summary>
	public const string UsageColumnName = "usage";
}
