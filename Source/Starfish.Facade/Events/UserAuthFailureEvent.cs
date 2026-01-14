using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Facade.Events;

/// <summary>
/// Defines the event that occurs when the user authentication fails.
/// </summary>
internal class UserAuthFailureEvent : ApplicationEvent
{
	/// <summary>
	/// Gets or sets the auth type.
	/// </summary>
	public string GrantType { get; set; }

	/// <summary>
	/// Gets or sets the time when the grant was attempted.
	/// </summary>
	public DateTime GrantTime { get; set; }

	/// <summary>
	/// Gets or sets the additional data.
	/// </summary>
	public Dictionary<string, string> Data { get; set; }

	/// <summary>
	/// Gets or sets the error message.
	/// </summary>
	public string Error { get; set; }

	/// <summary>
	/// Gets or sets the source of the authentication failure.
	/// </summary>
	public string Source { get; set; }
}