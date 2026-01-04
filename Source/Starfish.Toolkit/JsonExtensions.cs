using System.Text.Json;
using System.Text.Json.Nodes;

namespace Nerosoft.Starfish.Toolkit;

/// <summary>
/// Provides extension methods for <see cref="JsonNode"/> to retrieve typed values by path.
/// </summary>
/// <remarks>
/// This static class is stateless and provides convenience helpers to navigate
/// <see cref="JsonObject"/> and <see cref="JsonArray"/> structures using either a
/// delimited path (dots or slashes) or a sequence of path segments. When a path
/// cannot be resolved, the methods return the default value of specified value type.
/// </remarks>
public static class JsonExtensions
{
	/// <summary>
	/// Retrieves a value of type <typeparamref name="TValue"/> from the provided <see cref="JsonNode"/>
	/// using a single delimited path string. The path may use '.' or '/' as separators.
	/// </summary>
	/// <typeparam name="TValue">The expected return type.</typeparam>
	/// <param name="node">The root <see cref="JsonNode"/> to search. Must not be <c>null</c>.</param>
	/// <param name="name">
	/// A delimited path specifying the property/array traversal, for example "user.address.street" or "items/0/name".
	/// Must not be <c>null</c> or empty.
	/// </param>
	/// <returns>
	/// The value converted to <typeparamref name="TValue"/> if the path exists and conversion succeeds;
	/// otherwise the default value of <typeparamref name="TValue"/>.
	/// </returns>
	/// <exception cref="ArgumentNullException"><paramref name="node"/> is <c>null</c>.</exception>
	/// <exception cref="ArgumentException"><paramref name="name"/> is <c>null</c> or empty.</exception>
	/// <remarks>
	/// This method splits the provided <paramref name="name"/> on '.' and '/' and delegates
	/// to the overload that accepts path segments.
	/// </remarks>
	public static TValue GetValue<TValue>(this JsonNode node, string name)
	{
		if (node is null)
		{
			throw new ArgumentNullException(nameof(node), "JsonNode cannot be null.");
		}
		if (string.IsNullOrEmpty(name))
		{
			throw new ArgumentException("Name cannot be null or empty.", nameof(name));
		}

		var names = name.Split('.', '/');
		return GetValue<TValue>(node, names);
	}

	/// <summary>
	/// Retrieves a value of type <typeparamref name="TValue"/> from the provided <see cref="JsonNode"/>
	/// by traversing the JSON structure using the provided path segments.
	/// </summary>
	/// <typeparam name="TValue">The expected return type.</typeparam>
	/// <param name="node">The root <see cref="JsonNode"/> to search. Must not be <c>null</c>.</param>
	/// <param name="names">
	/// An ordered array of path segments. Each segment may be:
	/// - a property name (string) when the current node is a <see cref="JsonObject"/>, or
	/// - an array index (integer or string that parses to int) when the current node is a <see cref="JsonArray"/>.
	/// </param>
	/// <returns>
	/// The value converted to <typeparamref name="TValue"/> if the final node is a <see cref="JsonValue"/>
	/// or the final <see cref="JsonObject"/> can be deserialized to <typeparamref name="TValue"/>.
	/// If the path does not exist or the final node cannot be converted, the default value of
	/// <typeparamref name="TValue"/> is returned.
	/// </returns>
	/// <exception cref="ArgumentNullException"><paramref name="node"/> is <c>null</c>.</exception>
	/// <exception cref="ArgumentException"><paramref name="names"/> is <c>null</c> or empty.</exception>
	/// <remarks>
	/// The traversal algorithm:
	/// 1. Start at <paramref name="node"/>.
	/// 2. For each segment in <paramref name="names"/>:
	///    a. If the current node is a <see cref="JsonObject"/>, attempt to get the property with the segment's string representation.
	///    b. Else if the current node is a <see cref="JsonArray"/>, attempt to parse the segment as an integer index and access the element.
	///    c. If neither step succeeds, return the default value for <typeparamref name="TValue"/>.
	/// 3. After successful traversal, attempt to return:
	///    - <see cref="JsonValue"/> converted via <see cref="JsonValue.GetValue{TValue}"/>, or
	///    - <see cref="JsonObject"/> deserialized via <see cref="JsonObject.Deserialize{TValue}"/>.
	/// 4. Otherwise, return default.
	/// </remarks>
	public static TValue GetValue<TValue>(this JsonNode node, params object[] names)
	{
		if (node is null)
		{
			throw new ArgumentNullException(nameof(node), "JsonNode cannot be null.");
		}
		if (names is null || names.Length == 0)
		{
			throw new ArgumentException("At least one name must be provided.", nameof(names));
		}

		JsonNode currentNode = node;
		foreach (var name in names)
		{
			if (currentNode is JsonObject jsonObject && jsonObject.TryGetPropertyValue(name.ToString(), out var nextNode))
			{
				currentNode = nextNode;
			}
			else if (currentNode is JsonArray jsonArray && int.TryParse(name.ToString(), out int index) && index >= 0 && index < jsonArray.Count)
			{
				currentNode = jsonArray[index];
			}
			else
			{
				return default; // Return default value if the path does not exist
			}
		}

		return currentNode switch
		{
			JsonValue jsonValue => jsonValue.GetValue<TValue>(),
			JsonObject jsonObject => jsonObject.Deserialize<TValue>(),
			_ => default // Return default value if the final node is not a JsonValue or JsonObject
		};
	}
}