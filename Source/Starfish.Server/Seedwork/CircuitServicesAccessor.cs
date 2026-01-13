namespace Nerosoft.Starfish.Server;

/// <summary>
/// Accessor for the current circuit's service provider.
/// </summary>
public class CircuitServicesAccessor
{
	private static readonly AsyncLocal<IServiceProvider> _blazorServices = new();

	/// <summary>
	/// Gets or sets the current circuit's service provider.
	/// </summary>
	public IServiceProvider Services
	{
		get => _blazorServices.Value;
		set => _blazorServices.Value = value!;
	}
}