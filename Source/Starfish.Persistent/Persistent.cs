namespace Nerosoft.Starfish.Persistent;

/// <summary>
/// Represents an abstract base class for persistent entities with a generic identifier.
/// </summary>
/// <typeparam name="TKey">The type of the entity identifier.</typeparam>
public abstract class Persistent<TKey>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="Persistent{TKey}"/> class.
	/// </summary>
	protected Persistent()
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Persistent{TKey}"/> class with the specified identifier.
	/// </summary>
	/// <param name="id">The unique identifier for the entity.</param>
	protected Persistent(TKey id)
		: this()
	{
		Id = id;
	}

	/// <summary>
	/// Gets the unique identifier of the entity.
	/// </summary>
	/// <value>
	/// The unique identifier of type <typeparamref name="TKey"/>.
	/// </value>
	public TKey Id { get; private set; }
}