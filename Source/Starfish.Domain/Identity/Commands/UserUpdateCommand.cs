using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Domain.Commands;

/// <summary>
/// Represents a request to update an existing user within the domain.
/// </summary>
public class UserUpdateCommand : Command
{
	/// <summary>
	/// Gets or sets the unique identifier of the user to be updated.
	/// </summary>
	public string Id { get; set; }

	/// <summary>
	/// Gets or sets the new nickname for the user.
	/// </summary>
	public string Nickname { get; set; }

	/// <summary>
	/// Gets or sets the new email address for the user.
	/// </summary>
	public string Email { get; set; }

	/// <summary>
	/// Gets or sets the new phone number for the user.
	/// </summary>
	public string Phone { get; set; }
}
