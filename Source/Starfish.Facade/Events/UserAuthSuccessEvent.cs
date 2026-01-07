using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Facade.Events;

/// <summary>
/// Defines the event that occurs when the user authentication is successful.
/// </summary>
internal class UserAuthSuccessEvent : ApplicationEvent
{
	/// <summary>
	/// Initializes a new instance of the <see cref="UserAuthSuccessEvent"/> class.
	/// </summary>
	public UserAuthSuccessEvent()
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="UserAuthSuccessEvent"/> class.
	/// </summary>
	/// <param name="authType"></param>
	/// <param name="userId"></param>
	/// <param name="data"></param>
	public UserAuthSuccessEvent(string authType, string userId, Dictionary<string, string> data)
	{
		AuthType = authType;
		Data = data;
		UserId = userId;
	}

	/// <summary>
	/// Gets or sets the auth type.
	/// </summary>
	public string AuthType { get; set; }

	/// <summary>
	/// Gets or sets the additional data.
	/// </summary>
	public Dictionary<string, string> Data { get; set; }

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
	public DateTime TokenIssueTime { get; set; }
}