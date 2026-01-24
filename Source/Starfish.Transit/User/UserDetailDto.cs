namespace Nerosoft.Starfish.Transit;

/// <summary>
/// Defines the user detail data transfer object.
/// </summary>
public class UserDetailDto
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
	/// Gets or sets a value indicating whether the user is enabled.
	/// </summary>
	public int AccessFailedCount { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether the user is enabled.
	/// </summary>
	public DateTime? LockoutEnd { get; set; }

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
	/// Gets or sets the IP address from which the user last logged in.
	/// </summary>
	public string LastLoginIp { get; set; }

	/// <summary>
	/// Gets or sets the time when the password was last changed.
	/// </summary>
	public DateTime? PasswordChangedAt { get; set; }

	/// <summary>
	/// Gets or sets the roles assigned to the user.
	/// </summary>
	public HashSet<string> Roles { get; set; }
}