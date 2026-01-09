using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Domain.Commands;

/// <summary>
/// Represents a request to create a user within the domain.
/// </summary>
public class UserCreateCommand : Command
{
	public string Username { get; set; }

	public string Password { get; set; }

	public string Email { get; set; }

	public string Phone { get; set; }

	public string Nickname { get; set; }

	public List<string> Roles { get; set; }
}
