using Nerosoft.Euonia.Repository;

namespace Nerosoft.Starfish.Repository.Entities;

/// <summary>
/// Represents a token entity used for authentication and authorization purposes.
/// </summary>
internal sealed class Token : Entity<string>
{
	/// <summary>
	/// Gets or sets the token type (e.g., access_token, refresh_token).
	/// </summary>
	public string Type { get; set; }

	/// <summary>
	/// Gets or sets the key that is SHA256 encrypted.
	/// </summary>
	public string Key { get; set; }

	/// <summary>
	/// Gets or sets the subject associated with the token (e.g., user ID).
	/// </summary>
	public string Subject { get; set; }

	/// <summary>
	/// Gets or sets the issue time of the token.
	/// </summary>
	public DateTime Issues { get; set; }

	/// <summary>
	/// Gets or sets the expiration time of the token.
	/// </summary>
	public DateTime? Expires { get; set; }
}