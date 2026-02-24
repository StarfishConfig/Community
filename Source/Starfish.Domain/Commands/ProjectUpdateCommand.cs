using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Domain.Commands;

/// <summary>
/// Represents a command to update the details of an existing project entity.
/// </summary>
public class ProjectUpdateCommand : Command
{
	/// <summary>
	/// Initializes a new instance of the ProjectUpdateCommand class with the specified project identifier.
	/// </summary>
	/// <param name="id">The unique identifier of the project to be updated.</param>
	public ProjectUpdateCommand(long id)
	{
		Id = id;
	}

	/// <summary>
	/// Gets the unique identifier for the entity.
	/// </summary>
	public long Id { get; }

	/// <summary>
	/// Gets or sets the name of the project.
	/// </summary>
	/// <value>The project name.</value>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the image URL or identifier for the project.
	/// </summary>
	/// <value>The project image.</value>
	public string Image { get; set; }

	/// <summary>
	/// Gets or sets the description of the project.
	/// </summary>
	/// <value>The project description.</value>
	public string Description { get; set; }
}
