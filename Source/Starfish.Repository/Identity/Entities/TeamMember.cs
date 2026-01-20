using Nerosoft.Euonia.Repository;

namespace Nerosoft.Starfish.Repository.Entities;

/// <summary>
/// Represents a membership of a user in a team.
/// </summary>
/// <remarks>
/// Internal repository entity that inherits from <see cref="Entity{TKey}"/> with a long key
/// and implements <see cref="IHasCreateTime"/> to expose the creation timestamp.
/// </remarks>
internal class TeamMember : Entity<long>, IHasCreateTime
{
	/// <summary>
	/// Gets or sets the identifier of the team this member belongs to.
	/// </summary>
	public long TeamId { get; set; }

	/// <summary>
	/// Gets or sets the identifier of the user associated with this team membership.
	/// </summary>
	/// <remarks>
	/// Stored as a string to accommodate different user id formats (e.g. GUID, external provider id).
	/// </remarks>
	public string UserId { get; set; }

	/// <summary>
	/// Gets or sets the date and time when this team membership was created.
	/// </summary>
	/// <remarks>
	/// Satisfies the <see cref="IHasCreateTime"/> contract.
	/// </remarks>
	public DateTime CreatedAt { get; set; }
}