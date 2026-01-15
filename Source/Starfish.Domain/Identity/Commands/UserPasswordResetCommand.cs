using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Domain.Commands;

/// <summary>
/// Represents a request to reset a user's password within the domain.
/// </summary>
public class UserPasswordResetCommand : Command
{
	/// <summary>
	/// Initializes a new instance of the <see cref="UserPasswordResetCommand"/> class.
	/// </summary>
	/// <param name="id"></param>
	/// <param name="password"></param>
	public UserPasswordResetCommand(string id, string password)
	{
		Id = id;
		Password = password;
	}

	/// <summary>
	/// Gets or sets the identifier of the user whose password is to be reset.
	/// </summary>
	public string Id { get; set; }

	/// <summary>
	/// Gets or sets the new password for the user.
	/// </summary>
	public string Password { get; set; }

	/// <summary>
	/// Gets or sets the identifier of the administrator or system that initiated the password reset.
	/// </summary>
	public string ChangedBy { get; set; }
}