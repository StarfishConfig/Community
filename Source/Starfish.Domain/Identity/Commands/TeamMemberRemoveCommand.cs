using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Domain.Commands;

/// <summary>
/// Represents a command to remove one or more members from a team.
/// Inherits from <see cref="Command"/> and carries the target team identifier
/// and the list of user identifiers to remove.
/// </summary>
public class TeamMemberRemoveCommand : Command
{
	/// <summary>
	/// Initializes a new instance of the <see cref="TeamMemberRemoveCommand"/> class.
	/// Copies the provided <paramref name="userIds"/> into an internal list.
	/// </summary>
	/// <param name="id">The identifier of the team from which members will be removed.</param>
	/// <param name="userIds">A collection of user identifiers to remove from the team.</param>
	public TeamMemberRemoveCommand(long id, IEnumerable<string> userIds)
	{
		Id = id;
		UserIds = new List<string>(userIds);
	}

	/// <summary>
	/// Gets the identifier of the team targeted by this command.
	/// </summary>
	public long Id { get; }

	/// <summary>
	/// Gets the list of user identifiers to be removed from the team.
	/// The list is a copy of the enumerable provided to the constructor.
	/// </summary>
	public List<string> UserIds { get; }
}