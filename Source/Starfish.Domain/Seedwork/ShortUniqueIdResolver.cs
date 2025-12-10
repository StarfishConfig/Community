using System.Linq.Expressions;
using AutoMapper;

namespace Nerosoft.Starfish.Domain;

/// <summary>
/// Resolves a short unique identifier by encoding the source object's Id property to a base-encoded string.
/// </summary>
/// <typeparam name="TSource">The type of the source object.</typeparam>
/// <typeparam name="TDestination">The type of the destination object.</typeparam>
internal class ShortUniqueIdResolver<TSource, TDestination> : IValueResolver<TSource, TDestination, string>
{
	/// <summary>
	/// Resolves the short unique identifier from the source object's Id property.
	/// </summary>
	/// <param name="source">The source object containing the Id property.</param>
	/// <param name="destination">The destination object.</param>
	/// <param name="destMember">The destination member being resolved.</param>
	/// <param name="context">The resolution context.</param>
	/// <returns>A base-encoded string representation of the source object's Id.</returns>
	public string Resolve(TSource source, TDestination destination, string destMember, ResolutionContext context)
	{
		var id = GetId(source);
		return ShortUniqueId.Default.EncodeInt64(id);
	}

	/// <summary>
	/// Extracts the Id property value from the source object using expression trees.
	/// </summary>
	/// <param name="source">The source object to extract the Id from.</param>
	/// <returns>The long value of the Id property.</returns>
	private static long GetId(TSource source)
	{
		var property = Expression.PropertyOrField(Expression.Constant(source), "Id");
		var lambda = Expression.Lambda<Func<TSource, long>>(property, Expression.Parameter(typeof(TSource), "source")).Compile();
		return lambda(source);
	}
}