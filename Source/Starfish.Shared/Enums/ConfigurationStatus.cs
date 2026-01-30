namespace Nerosoft.Starfish.Shared;

/// <summary>
/// Defines the configuration status enums.
/// </summary>
public enum ConfigurationStatus
{
	/// <summary>
	/// Indicates no status.
	/// </summary>
	None = 0,

	/// <summary>
	/// Indicates pending status.
	/// </summary>
	Pending = 1,

	/// <summary>
	/// Indicates published status.
	/// </summary>
	Published = 2,

	/// <summary>
	/// Indicates deprecated status.
	/// </summary>
	Deprecated = 3
}