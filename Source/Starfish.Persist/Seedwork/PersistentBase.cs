using Nerosoft.Euonia.Repository;

namespace Nerosoft.Starfish.Persist;

internal abstract class PersistentBase<TKey> : IPersistent<TKey>
	where TKey : IEquatable<TKey>
{
	public object[] GetKeys()
	{
		return [Id!];
	}

	public TKey Id { get; set; }
}