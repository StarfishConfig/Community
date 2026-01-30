namespace Nerosoft.Starfish.Infrastructure;

/// <summary>
/// Specifies the type of team member.
/// </summary>
public enum TeamMemberRole
{
	/// <summary>
	/// No specific role assigned.
	/// </summary>
	None = 0,

	/// <summary>
	/// Team owner with full permissions.
	/// </summary>
	Owner = 1,

	/// <summary>
	/// Ordinary team member with standard permissions.
	/// </summary>
	Ordinary = 2,
}