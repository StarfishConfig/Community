using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Facade.Events;

/// <summary>
/// Raised when a member is removed from a team.
/// </summary>
/// <remarks>
/// Inherits from <see cref="ApplicationEvent"/> and carries the identifiers of the
/// team and the user that was removed. Consumers can handle this event to react
/// to membership changes (e.g., update projections, notify other systems).
/// </remarks>
internal class TeamMemberRemovedEvent : ApplicationEvent
{
	private readonly List<string> _userIds = [];

	public TeamMemberRemovedEvent(long teamId, params string[] userIds)
	{
		TeamId = teamId;
		_userIds.AddRange(userIds);
	}

	/// <summary>
	/// Gets the identifier of the team from which the member was removed.
	/// </summary>
	public long TeamId { get; }

	/// <summary>
	/// Gets the list of user identifiers that were removed from the team.
	/// </summary>
	public IList<string> UserIds => _userIds;
}