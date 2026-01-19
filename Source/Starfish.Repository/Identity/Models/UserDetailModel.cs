namespace Nerosoft.Starfish.Repository.Models;

/// <summary>
/// Represents the detailed information of a user for query operations.
/// </summary>
public class UserDetailModel
{
	/// <summary>
	/// Gets or sets the user identifier.
	/// </summary>
	public string Id { get; set; }

	/// <summary>
	/// Gets or sets the username.
	/// </summary>
	public string Username { get; set; }

	/// <summary>
	/// Gets or sets the email address.
	/// </summary>
	public string Email { get; set; }

	/// <summary>
	/// Gets or sets the phone number.
	/// </summary>
	public string Phone { get; set; }

	/// <summary>
	/// Gets or sets the nickname.
	/// </summary>
	public string Nickname { get; set; }

	/// <summary>
	/// Gets or sets the time when the user was created.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the time when the user was last updated.
	/// </summary>
	public DateTime UpdatedAt { get; set; }

	/// <summary>
	/// Gets or sets the time when the user last logged in.
	/// </summary>
	public DateTime? LastLoginAt { get; set; }

	/// <summary>
	/// Gets or sets the time when the password was last changed.
	/// </summary>
	public DateTime? PasswordChangedAt { get; set; }

	/// <summary>
	/// Gets or sets the roles assigned to the user.
	/// </summary>
	public HashSet<string> Roles { get; set; }
}