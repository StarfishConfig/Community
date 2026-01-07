using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Facade.Events;

/// <summary>
/// Defines the event that occurs when the access token is refreshed.
/// </summary>
internal class TokenRefreshedEvent : ApplicationEvent
{
	/// <summary>
	/// The origin access token.
	/// </summary>
	public string OriginToken { get; set; }
}