using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Domain.Commands;

/// <summary>
/// Represents a command to request deletion of an existing team.
/// Inherits from <see cref="Command"/> which provides command metadata (e.g., correlation id).
/// </summary>
public class TeamDeleteCommand : Command
{
	/// <summary>
	/// Initializes a new instance of the <see cref="TeamDeleteCommand"/> class with the specified team identifier.
	/// </summary>
	/// <param name="id">The unique numeric identifier of the team to delete.</param>
	public TeamDeleteCommand(long id)
	{
		Id = id;
	}

	/// <summary>
	/// Gets the unique identifier of the team to be deleted.
	/// </summary>
	public long Id { get; }
}