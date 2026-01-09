using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Domain.Commands;

/// <summary>
/// Represents a request to change a user's password within the domain.
/// </summary>
public class UserPasswordChangeCommand : Command
{
	/// <summary>
	/// Gets or sets the identifier of the user whose password is to be changed.
	/// </summary>
	public string Id { get; set; }

	/// <summary>
	/// Gets or sets the new password for the user.
	/// </summary>
	public string Password { get; set; }
}
