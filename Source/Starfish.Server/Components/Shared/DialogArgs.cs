namespace Nerosoft.Starfish.Server.Components.Shared;

/// <summary>
/// Represents the arguments for a dialog with a specific key type.
/// </summary>
/// <typeparam name="TKey">The type of the key associated with the dialog.</typeparam>
public class DialogArgs<TKey> : DialogArgs
{
	/// <summary>
	/// Gets or sets the identifier associated with the dialog.
	/// </summary>
	public TKey Id { get; set; }
}

/// <summary>
/// Represents the base arguments for a dialog, containing additional data.
/// </summary>
public class DialogArgs
{
	/// <summary>
	/// Gets a dictionary containing additional data for the dialog.
	/// </summary>
	public Dictionary<string, object> Data { get; } = new();
}