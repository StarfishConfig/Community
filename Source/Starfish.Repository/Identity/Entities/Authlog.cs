using Nerosoft.Euonia.Repository;

namespace Nerosoft.Starfish.Repository.Entities;

/// <summary>
/// Represents an authentication event log entry persisted by the repository.
/// </summary>
/// <remarks>
/// Contains information about authentication attempts (successful or failed).
/// Some fields may contain sensitive data (for example, <see cref="IpAddress"/> and <see cref="UserAgent"/>)
/// and should be handled according to security and privacy policies (restricted access, redaction, or encryption).
/// </remarks>
internal class Authlog : Entity<string>
{
	/// <summary>
	/// Identifier of the user associated with the authentication event.
	/// </summary>
	public string UserId { get; set; }

	/// <summary>
	/// Username presented during the authentication attempt.
	/// May be null or empty for anonymous/unknown attempts.
	/// </summary>
	public string Username { get; set; }

	/// <summary>
	/// The grant or authentication method used (e.g., "password", "refresh_token", "oauth2").
	/// </summary>
	public string GrantType { get; set; }

	/// <summary>
	/// Correlation or request identifier for tracing the authentication operation across systems.
	/// </summary>
	public string RequestId { get; set; }

	/// <summary>
	/// Source IP address from which the authentication request originated.
	/// This is sensitive information and should be protected.
	/// </summary>
	public string IpAddress { get; set; }

	/// <summary>
	/// User agent string from the client initiating the authentication request.
	/// May contain sensitive or identifying information.
	/// </summary>
	public string UserAgent { get; set; }

	/// <summary>
	/// Referer URL from which the authentication request was made.
	/// </summary>
	public string Referer { get; set; }

	/// <summary>
	/// Name of the application or service where the authentication attempt occurred.
	/// </summary>
	public string AppName { get; set; }

	/// <summary>
	/// Version of the application or service where the authentication attempt occurred.
	/// </summary>
	public string AppVersion { get; set; }

	/// <summary>
	/// Operating system platform of the client making the authentication request.
	/// </summary>
	public string OsPlatform { get; set; }
	
	/// <summary>
	/// Gets or sets the source of the authentication failure.
	/// </summary>
	public string Source { get; set; }

	/// <summary>
	/// Indicates whether the authentication attempt succeeded.
	/// </summary>
	public bool Success { get; set; }

	/// <summary>
	/// Optional remark or message associated with the authentication event (error details, reason, etc.).
	/// Avoid storing raw sensitive data in this field.
	/// </summary>
	public string Remark { get; set; }

	/// <summary>
	/// Timestamp when the log entry was created (UTC recommended).
	/// </summary>
	public DateTime Timestamp { get; set; }
}