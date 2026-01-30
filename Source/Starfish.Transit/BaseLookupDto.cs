namespace Nerosoft.Starfish.Transit;

/// <summary>
/// Base DTO for simple lookup entries containing an identifier and a display name.
/// </summary>
/// <typeparam name="TKey">The type of the identifier for the lookup entry.</typeparam>
public abstract class BaseLookupDto<TKey>
{
	/// <summary>
	/// Gets or sets the identifier of the lookup entry.
	/// </summary>
	/// <value>The identifier value of type <typeparamref name="TKey"/>.</value>
	public TKey Id { get; set; }

	/// <summary>
	/// Gets or sets the display name of the lookup entry.
	/// </summary>
	/// <value>A human-readable name associated with the identifier.</value>
	public string Name { get; set; }
}