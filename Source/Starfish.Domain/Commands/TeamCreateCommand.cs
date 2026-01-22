using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Domain.Commands;

/// <summary>
/// Command used to request creation of a team.
/// Inherits from <see cref="Command"/> which provides command metadata (e.g., correlation id).
/// </summary>
public class TeamCreateCommand : Command
{
	/// <summary>
	/// Gets or sets the display name of the team to create.
	/// Expected to be a human-readable name; validation (length, uniqueness) is handled elsewhere.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets an optional description for the team.
	/// Can include details about purpose, scope, or other metadata.
	/// </summary>
	public string Description { get; set; }
}