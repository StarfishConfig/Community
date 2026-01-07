namespace Nerosoft.Starfish.Repository.Models;

/// <summary>
/// Represents a query model for user authentication/identity lookups.
/// Contains identifying fields and the list of roles associated with the user.
/// </summary>
public class UserAuthQueryModel
{
	/// <summary>
	/// Gets or sets the unique identifier of the user.
	/// </summary>
	public string Id { get; set; }

	/// <summary>
	/// Gets or sets the username used for authentication or display.
	/// </summary>
	public string Username { get; set; }

	/// <summary>
	/// Gets or sets the user's email address.
	/// </summary>
	public string Email { get; set; }

	/// <summary>
	/// Gets or sets the user's phone number.
	/// </summary>
	public string Phone { get; set; }

	/// <summary>
	/// Gets or sets the user's chosen nickname.
	/// </summary>
	public string Nickname { get; set; }

	/// <summary>
	/// Gets or sets the collection of role names assigned to the user.
	/// </summary>
	public HashSet<string> Roles { get; set; } = [];
}