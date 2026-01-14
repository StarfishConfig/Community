using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Domain.Commands;

/// <summary>
/// Represents a request to change a user's password within the domain.
/// </summary>
/// <remarks>
/// Consumers should validate the current password, enforce password strength requirements,
/// and ensure the new password is handled securely (hashed at rest and not logged in plaintext).
/// </remarks>
public class UserPasswordChangeCommand : Command
{
	/// <summary>
	/// Initializes a new instance of the <see cref="UserPasswordChangeCommand"/> class.
	/// </summary>
	/// <param name="id">The identifier of the user whose password will be changed.</param>
	/// <param name="password">The new password for the user.</param>
	public UserPasswordChangeCommand(string id, string password)
	{
		Id = id;
		Password = password;
	}

	/// <summary>
	/// Gets or sets the identifier of the user whose password is to be changed.
	/// </summary>
	public string Id { get; set; }

	/// <summary>
	/// Gets or sets the new plaintext password for the user.
	/// </summary>
	/// <remarks>
	/// Validate strength and securely persist (hash) the new password. Avoid exposing this value in logs or error messages.
	/// </remarks>
	public string Password { get; set; }
}