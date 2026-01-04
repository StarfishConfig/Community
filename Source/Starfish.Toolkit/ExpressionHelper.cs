using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace Nerosoft.Starfish.Toolkit;

/// <summary>
/// Provides fast access to object properties using compiled expression trees and a shared cache.
/// </summary>
/// <remarks>
/// The helper compiles delegates that read properties or fields by name and stores them in a thread-safe
/// <see cref="ConcurrentDictionary{TKey, TValue}"/> to avoid repeated compilation. Callers should ensure
/// the provided property name exists on the source type; otherwise an exception will be thrown
/// when the expression is built or executed.
/// </remarks>
public class ExpressionHelper
{
    /// <summary>
    /// Cache mapping a unique key (type full name + property name) to a compiled delegate that reads the property.
    /// </summary>
    private static readonly ConcurrentDictionary<string, Delegate> _cache = new();

    /// <summary>
    /// Gets the value of the named property from a source instance, strongly typed to the expected property type.
    /// </summary>
    /// <typeparam name="TSource">The type of the source object.</typeparam>
    /// <typeparam name="T">The expected type of the property value.</typeparam>
    /// <param name="source">The instance to read the property from. Passing <c>null</c> will typically result in a <see cref="NullReferenceException"/> when the compiled delegate is invoked for instance members.</param>
    /// <param name="propertyName">The name of the property or field to read. Must be a valid member of <typeparamref name="TSource"/>.</param>
    /// <returns>The property value cast to <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown by underlying APIs if <paramref name="propertyName"/> is null.</exception>
    /// <exception cref="InvalidOperationException">May be thrown if the member cannot be bound or converted to <typeparamref name="T"/>.</exception>
    public static T GetPropertyValue<TSource, T>(TSource source, string propertyName)
    {
        var key = $"{typeof(TSource).FullName}.{propertyName}";

        var func = (Func<TSource, T>)_cache.GetOrAdd(key, _ =>
        {
            var parameter = Expression.Parameter(typeof(TSource), "x");
            var propertyExpression = Expression.PropertyOrField(parameter, propertyName);
            var lambda = Expression.Lambda<Func<TSource, T>>(propertyExpression, parameter);
            return lambda.Compile();
        });

        return func(source);
    }

    /// <summary>
    /// Gets the value of the named property from a source instance and returns it as <see cref="object"/>.
    /// </summary>
    /// <typeparam name="TSource">The type of the source object.</typeparam>
    /// <param name="source">The instance to read the property from.</param>
    /// <param name="propertyName">The name of the property or field to read.</param>
    /// <returns>The property value boxed as <see cref="object"/>.</returns>
    /// <remarks>
    /// This overload is useful when the caller does not know the property type at compile time.
    /// The returned value will be <c>null</c> if the property value is <c>null</c>; type conversions are handled
    /// by an expression that converts the property value to <see cref="object"/>.
    /// </remarks>
    public static object GetPropertyValue<TSource>(TSource source, string propertyName)
    {
        var key = $"{typeof(TSource).FullName}.{propertyName}";

        var func = (Func<TSource, object>)_cache.GetOrAdd(key, _ =>
        {
            var parameter = Expression.Parameter(typeof(TSource), "source");
            var propertyExpression = Expression.PropertyOrField(parameter, propertyName);
            var converted = Expression.Convert(propertyExpression, typeof(object));
            var lambda = Expression.Lambda<Func<TSource, object>>(converted, parameter);
            return lambda.Compile();
        });

        return func(source);
    }

    /// <summary>
    /// Gets the value of the named property from a runtime object instance and returns it as <see cref="object"/>.
    /// </summary>
    /// <param name="source">The instance to read the property from. Its runtime type is used to build the accessor.</param>
    /// <param name="propertyName">The name of the property or field to read.</param>
    /// <returns>The property value boxed as <see cref="object"/>.</returns>
    /// <remarks>
    /// This overload accepts an <see cref="object"/> instance when the compile-time type is not available.
    /// The cache key includes the runtime type full name to avoid delegate type mismatches across different types.
    /// </remarks>
    public static object GetPropertyValue(object source, string propertyName)
    {
        var type = source.GetType();

        var key = $"{source.GetType().FullName}.{propertyName}";

        var func = (Func<object, object>)_cache.GetOrAdd(key, _ =>
        {
            var parameter = Expression.Parameter(type, "source");
            var convertedSource = Expression.Convert(parameter, source.GetType());
            var propertyExpression = Expression.PropertyOrField(convertedSource, propertyName);
            var converted = Expression.Convert(propertyExpression, typeof(object));
            var lambda = Expression.Lambda<Func<object, object>>(converted, parameter);
            return lambda.Compile();
        });

        return func(source);
    }
}