using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Domain.Commands;

/// <summary>
/// Represents a request to create a token within the domain.
/// Inherits from <see cref="Command"/> which provides common command behavior.
/// </summary>
public class TokenCreateCommand : Command
{
	/// <summary>
	/// Gets or sets the token type (for example: "access", "refresh").
	/// </summary>
	public string Type { get; set; }

	/// <summary>
	/// Gets or sets the token value/string.
	/// </summary>
	public string Token { get; set; }

	/// <summary>
	/// Gets or sets the subject (owner) of the token, typically a user identifier.
	/// </summary>
	public string Subject { get; set; }

	/// <summary>
	/// Gets or sets the UTC date and time when the token was issued.
	/// </summary>
	public DateTime Issues { get; set; }

	/// <summary>
	/// Gets or sets the optional UTC date and time when the token expires.
	/// If <c>null</c>, the token does not have an expiration.
	/// </summary>
	public DateTime? Expires { get; set; }
}