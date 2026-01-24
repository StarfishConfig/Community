namespace Nerosoft.Starfish.Repository.Models;

/// <summary>
/// Represents the search criteria for filtering authentication logs.
/// </summary>
public class AuthlogCriteriaModel
{
	/// <summary>
	/// Gets or sets the user identifier to filter authentication logs.
	/// </summary>
	public string UserId { get; set; }

	/// <summary>
	/// Gets or sets the username to filter authentication logs.
	/// </summary>
	public string Username { get; set; }

	/// <summary>
	/// Gets or sets the authentication source to filter logs (e.g., application name or identity provider).
	/// </summary>
	public string Source { get; set; }

	/// <summary>
	/// Gets or sets the OAuth 2.0 grant type to filter authentication logs (e.g., "password", "authorization_code", "client_credentials").
	/// </summary>
	public string GrantType { get; set; }

	/// <summary>
	/// Gets or sets the start date and time for the authentication log time range filter.
	/// </summary>
	public DateTime? From { get; set; }

	/// <summary>
	/// Gets or sets the end date and time for the authentication log time range filter.
	/// </summary>
	public DateTime? To { get; set; }

	/// <summary>
	/// Gets or sets the authentication result filter. True for successful authentication, false for failed, or null for all results.
	/// </summary>
	public bool? Result { get; set; }
}
