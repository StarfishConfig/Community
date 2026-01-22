using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Domain.Commands;

/// <summary>
/// Command used to request an update to an existing team.
/// Inherits from <see cref="Command"/> which provides command metadata (e.g., correlation id).
/// </summary>
public class TeamUpdateCommand : Command
{
	/// <summary>
	/// Initializes a new instance of the <see cref="TeamUpdateCommand"/> class with the specified team identifier.
	/// </summary>
	/// <param name="id">The unique numeric identifier of the team to update.</param>
	public TeamUpdateCommand(long id)
	{
		Id = id;
	}

	/// <summary>
	/// Gets the unique identifier of the team being updated.
	/// </summary>
	public long Id { get; }

	/// <summary>
	/// Gets or sets the new display name for the team.
	/// Validation (e.g., length, uniqueness) is handled by the caller or validators.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the new description for the team.
	/// Optional; can be <c>null</c> or empty to indicate no change or removal.
	/// </summary>
	public string Description { get; set; }
}