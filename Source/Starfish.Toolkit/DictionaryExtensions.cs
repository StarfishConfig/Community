namespace Nerosoft.Starfish.Toolkit;

/// <summary>
/// Provides extension methods for working with <see cref="Dictionary{String,Object}"/>
/// in a type-safe manner.
/// </summary>
/// <remarks>
/// These extensions simplify retrieving values stored as <see cref="object"/> and attempting
/// to cast them to a specific type using safe pattern matching.
/// </remarks>
public static class DictionaryExtensions
{
	/// <summary>
	/// Attempts to retrieve the value associated with the specified <paramref name="key"/>
	/// from the <paramref name="dictionary"/> and cast it to <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">The expected type of the value.</typeparam>
	/// <param name="dictionary">
	/// The dictionary to search. The dictionary is expected to be non-null; calling this
	/// extension on a null reference will result in a <see cref="NullReferenceException"/>.
	/// </param>
	/// <param name="key">The key whose value to get.</param>
	/// <param name="value">
	/// When this method returns, contains the value associated with the specified key,
	/// if the key is found and the value is of type <typeparamref name="T"/>; otherwise, the default
	/// value for <typeparamref name="T"/>.
	/// </param>
	/// <returns>
	/// <see langword="true"/> if the dictionary contains an element with the specified key
	/// and the element is of type <typeparamref name="T"/>; otherwise, <see langword="false"/>.
	/// </returns>
	/// <remarks>
	/// This method uses pattern matching to verify the runtime type of the stored object.
	/// If the key is present but the value cannot be cast to <typeparamref name="T"/>, the method
	/// returns <see langword="false"/> and <paramref name="value"/> is set to <c>default</c>.
	/// </remarks>
	public static bool TryGetValue<T>(this Dictionary<string, object> dictionary, string key, out T value)
	{
		if (dictionary.TryGetValue(key, out var obj) && obj is T t)
		{
			value = t;
			return true;
		}

		value = default;
		return false;
	}
}