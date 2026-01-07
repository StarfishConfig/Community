using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Domain.Commands;

/// <summary>
/// Represents a command to delete a token.
/// </summary>
internal class TokenDeleteCommand : Command
{
	/// <summary>
	/// Gets or sets the type of the token to be deleted.
	/// </summary>
	public string Type { get; set; }

	/// <summary>
	/// Gets or sets the token value to be deleted.
	/// </summary>
	public string Token { get; set; }
}