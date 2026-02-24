using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Domain.Commands;

/// <summary>
/// Represents a command to delete an existing project entity.
/// </summary>
public class ProjectDeleteCommand : Command
{
	/// <summary>
	/// Initializes a new instance of the ProjectDeleteCommand class with the specified project identifier.
	/// </summary>
	/// <param name="id">The unique identifier of the project to be deleted.</param>
	public ProjectDeleteCommand(long id)
	{
		Id = id;
	}

	/// <summary>
	/// Gets the unique identifier for the entity.
	/// </summary>
	public long Id { get; }
}
