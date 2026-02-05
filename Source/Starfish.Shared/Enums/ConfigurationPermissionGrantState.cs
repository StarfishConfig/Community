namespace Nerosoft.Starfish.Shared;

/// <summary>
/// Bitwise flags representing permission types that can be granted for a configuration.
/// </summary>
/// <remarks>
/// Marked with the <c>[Flags]</c> attribute to allow combining multiple permission values using bitwise operations.
/// </remarks>
[Flags]
public enum ConfigurationPermissionGrantState
{
	/// <summary>
	/// No permissions are granted.
	/// </summary>
	None = 0,

	/// <summary>
	/// Grants read access to the configuration.
	/// </summary>
	Read = 1,

	/// <summary>
	/// Grants write (modify) access to the configuration.
	/// </summary>
	Write = 2,

	/// <summary>
	/// Grants the ability to publish the configuration.
	/// </summary>
	Publish = 4,

	/// <summary>
	/// All permissions: read, write and publish.
	/// </summary>
	All = Read | Write | Publish
}