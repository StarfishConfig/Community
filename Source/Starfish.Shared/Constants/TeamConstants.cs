namespace Nerosoft.Starfish.Shared;

/// <summary>
/// Contains constants related to team properties and constraints.
/// </summary>
public static class TeamConstants
{
	/// <summary>
	/// Team table name in the database.
	/// </summary>
	public const string TableName = "team";
	
	/// <summary>
	/// Owner ID length constraints.
	/// </summary>
	public const int OwnerIdLength = 64;

	/// <summary>
	/// Owner ID column name in the database.
	/// </summary>
	public const string OwnerIdColumnName = "owner_id";

	/// <summary>
	/// Owner ID index name in the database.
	/// </summary>
	public const string OwnerIdIndexName = "idx_team_owner_id";
	
	/// <summary>
	/// Team name length constraints(maximum).
	/// </summary>
	public const int NameMaximumLength = 100;

	/// <summary>
	/// Team name column name in the database.
	/// </summary>
	public const string NameColumnName = "name";
	
	/// <summary>
	/// Team description length constraints(maximum).
	/// </summary>
	public const int DescriptionMaximumLength = 500;
	
	/// <summary>
	/// Team description column name in the database.
	/// </summary>
	public const string DescriptionColumnName = "description";
	
	/// <summary>
	/// Team members count column name in the database.
	/// </summary>
	public const string MembersCountColumnName = "members_count";
}