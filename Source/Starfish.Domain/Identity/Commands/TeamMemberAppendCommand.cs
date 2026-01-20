using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Domain.Commands;

/// <summary>
/// Represents a command to append one or more members to a team.
/// Inherits from <see cref="Command"/> and carries the target team identifier
/// and the list of user identifiers to add.
/// </summary>
public class TeamMemberAppendCommand : Command
{
	/// <summary>
	/// Initializes a new instance of the <see cref="TeamMemberAppendCommand"/> class.
	/// Copies the provided <paramref name="userIds"/> into an internal list.
	/// </summary>
	/// <param name="id">The identifier of the team to which members will be appended.</param>
	/// <param name="userIds">A collection of user identifiers to add to the team.</param>
	public TeamMemberAppendCommand(long id, IEnumerable<string> userIds)
	{
		Id = id;
		UserIds = new List<string>(userIds);
	}

	/// <summary>
	/// Gets the identifier of the team targeted by this command.
	/// </summary>
	public long Id { get; }

	/// <summary>
	/// Gets the list of user identifiers to be appended to the team.
	/// The list is a copy of the enumerable provided to the constructor.
	/// </summary>
	public List<string> UserIds { get; }
}