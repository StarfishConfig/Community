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
	/// <param name="grantType"></param>
	/// <param name="userId"></param>
	/// <param name="data"></param>
	public UserAuthSuccessEvent(string grantType, string userId, Dictionary<string, string> data)
	{
		GrantType = grantType;
		Data = data;
		UserId = userId;
	}

	/// <summary>
	/// Gets or sets the auth type.
	/// </summary>
	public string GrantType { get; set; }

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
	/// Gets or sets the token issue time.
	/// </summary>
	public DateTime GrantTime { get; set; }
}