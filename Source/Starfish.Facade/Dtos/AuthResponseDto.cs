namespace Nerosoft.Starfish.Facade.Dtos;

/// <summary>
/// Defines the user authentication result data transfer object.
/// </summary>
public class AuthResponseDto
{
	/// <summary>
	/// Gets or sets the access token.
	/// </summary>
	public string AccessToken { get; set; }

	/// <summary>
	/// Gets or sets the refresh token.
	/// </summary>
	public string RefreshToken { get; set; }

	/// <summary>
	/// Gets or sets the token type.
	/// </summary>
	public string TokenType { get; set; }

	/// <summary>
	/// Gets or sets the expires time in seconds.
	/// </summary>
	public long ExpiresIn { get; set; }

	/// <summary>
	/// Gets or sets the token issues time.
	/// </summary>
	public long IssueAt { get; set; }
    
	/// <summary>
	/// Gets or sets the user unique identifier.
	/// </summary>
	public string UserId { get; set; }

	/// <summary>
	/// Gets or sets the username.
	/// </summary>
	public string Username { get; set; }
}
