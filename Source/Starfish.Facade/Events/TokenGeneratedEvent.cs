using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Facade.Events;

/// <summary>
/// Event triggered when a token is generated.
/// </summary>
internal class TokenGeneratedEvent : ApplicationEvent
{
	/// <summary>
	/// Gets or sets the user ID.
	/// </summary>
	public string UserId { get; set; }

	/// <summary>
	/// Gets or sets the username.
	/// </summary>
	public string Username { get; set; }

	/// <summary>
	/// Gets or sets the refresh token.
	/// </summary>
	public string RefreshToken { get; set; }

	/// <summary>
	/// Gets or sets the token issue time.
	/// </summary>
	public DateTime GrantTime { get; set; }
}