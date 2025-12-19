using System.Reflection;

namespace Nerosoft.Starfish.Facade;

/// <summary>
/// The service bus handler registration configuration.
/// </summary>
internal class MessageBusHandlerRegistration
{
	private readonly List<Assembly> _assemblies = [];

	public IReadOnlyList<Assembly> Assemblies => _assemblies;

	/// <summary>
	/// Adds the specified <see cref="Assembly"/> to the internal collection of assemblies
	/// that will be scanned for service bus handlers.
	/// </summary>
	/// <param name="assembly">The assembly that contains service bus handler types to register.</param>
	/// <returns>
	/// The same <see cref="MessageBusHandlerRegistration"/> instance to allow fluent configuration.
	/// </returns>
	public MessageBusHandlerRegistration AddAssembly(Assembly assembly)
	{
		_assemblies.Add(assembly);
		return this;
	}
}
