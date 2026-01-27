namespace Nerosoft.Starfish.Persistent;

/// <summary>
/// Represents a persisted team entity.
/// Inherits from <c>Persistent&lt;long&gt;</c> which provides the numeric identifier.
/// </summary>
public class TeamData : Persistent<long>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="TeamData"/> class.
	/// Creates an empty team record with default values.
	/// </summary>
	public TeamData()
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="TeamData"/> class with the specified identifier.
	/// </summary>
	/// <param name="id">The unique numeric identifier for the team.</param>
	public TeamData(long id)
		: base(id)
	{
	}

	/// <summary>
	/// Gets or sets the identifier of the user that owns the team.
	/// Typically, this is the user id of the team creator or primary administrator.
	/// </summary>
	public string OwnerId { get; set; }

	/// <summary>
	/// Gets or sets the display name of the team.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets a short description of the team's purpose or details.
	/// </summary>
	public string Description { get; set; }

	/// <summary>
	/// Gets or sets the set of member identifiers that belong to this team.
	/// Initialized to an empty <see cref="HashSet{T}"/> to avoid null references.
	/// </summary>
	public HashSet<string> Members { get; set; } = [];
}