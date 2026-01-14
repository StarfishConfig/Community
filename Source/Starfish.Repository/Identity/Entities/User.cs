using Nerosoft.Euonia.Repository;

namespace Nerosoft.Starfish.Repository.Entities;

/// <summary>
/// Represents a user entity in the persistence layer.
/// </summary>
internal class User : Entity<string>,
                      IHasCreateTime, IHasUpdateTime, IHasDeleteTime, ITombstone
{
	/// <summary>
	/// Gets or sets the username.
	/// </summary>
	public string Username { get; set; }

	/// <summary>
	/// Gets or sets the encrypted password.
	/// </summary>
	public string PasswordHash { get; set; }

	/// <summary>
	/// Gets or sets the salt used to encrypt the password.
	/// </summary>
	public string PasswordSalt { get; set; }

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
	/// Gets or sets the creation time.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the update time.
	/// </summary>
	public DateTime UpdatedAt { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether this instance is deleted.
	/// </summary>
	public bool IsDeleted { get; set; }

	/// <summary>
	/// Gets or sets the delete time.
	/// </summary>
	public DateTime? DeletedAt { get; set; }

	/// <summary>
	/// Gets or sets the time when the password was last changed.
	/// </summary>
	public DateTime? PasswordChangedAt { get; set; }

	/// <summary>
	/// Gets or sets the roles associated with the user.
	/// </summary>
	public HashSet<UserRole> Roles { get; set; }
}