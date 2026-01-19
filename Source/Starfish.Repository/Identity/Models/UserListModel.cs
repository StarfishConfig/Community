namespace Nerosoft.Starfish.Repository.Models;

/// <summary>
/// Represents a model for listing user information.
/// </summary>
public class UserListModel
{
	/// <summary>
	/// Gets or sets the unique identifier of the user.
	/// </summary>
	public string Id { get; set; }

	/// <summary>
	/// Gets or sets the username.
	/// </summary>
	public string Username { get; set; }

	/// <summary>
	/// Gets or sets the nickname.
	/// </summary>
	public string Nickname { get; set; }

	/// <summary>
	/// Gets or sets the email address.
	/// </summary>
	public string Email { get; set; }

	/// <summary>
	/// Gets or sets the phone number.
	/// </summary>
	public string Phone { get; set; }

	/// <summary>
	/// Gets or sets the count of failed access attempts.
	/// </summary>
	public int AccessFailedCount { get; set; }

	/// <summary>
	/// Gets or sets the lockout end time.
	/// </summary>
	public DateTime? LockoutEnd { get; set; }

	/// <summary>
	/// Gets or sets the time when the password was last changed.
	/// </summary>
	public DateTime? PasswordChangedAt { get; set; }

	/// <summary>
	/// Gets or sets the creation time.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// 
	/// </summary>
	public DateTime UpdatedAt { get; set; }
}