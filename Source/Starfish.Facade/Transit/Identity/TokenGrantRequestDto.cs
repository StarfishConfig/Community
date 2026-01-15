namespace Nerosoft.Starfish.Facade.Transit;

/// <summary>
/// Defines the user authentication request data transfer object.
/// </summary>
public class TokenGrantRequestDto
{
	/// <summary>
	/// Gets or sets the username.
	/// </summary>
	public string Username { get; set; }

	/// <summary>
	/// Gets or sets the password.
	/// </summary>
	public string Password { get; set; }

	/// <summary>
	/// Gets or sets the grant type.
	/// </summary>
	/// <value>Username/Email/Phone/Github/Microsoft/Google etc.</value>
	public string GrantType { get; set; }

	/// <summary>
	/// Gets or sets the request ID for the authentication request.
	/// </summary>
	public string RequestId { get; set; }
}