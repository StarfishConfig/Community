using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Domain.Commands;

/// <summary>
/// Represents a command to create a new project.
/// </summary>
public class ProjectCreateCommand : Command
{
	/// <summary>
	/// Gets or sets the identifier of the team that will own the project.
	/// </summary>
	/// <value>The team identifier.</value>
	public long TeamId { get; set; }

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