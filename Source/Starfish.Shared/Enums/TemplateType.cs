namespace Nerosoft.Starfish.Shared;

/// <summary>
/// Specifies the type of message template used for sending notifications.
/// </summary>
public enum TemplateType
{
	/// <summary>
	/// Specifies that no message template type is selected.
	/// </summary>
	None = 0,

	/// <summary>
	/// Specifies that the notification is sent by email.
	/// </summary>
	Email = 1,

	/// <summary>
	/// Short Message Service (SMS)
	/// </summary>
	Sms = 2,

	/// <summary>
	/// Represents a notification message type.
	/// </summary>
	Notification = 3,

	/// <summary>
	/// Represents an unspecified or unknown value.
	/// </summary>
	Other = 99
}