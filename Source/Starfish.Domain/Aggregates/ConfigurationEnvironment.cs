using Nerosoft.Euonia.Osba;
using Nerosoft.Starfish.Persistent.Data;

namespace Nerosoft.Starfish.Domain.Aggregates;

/// <summary>
/// Represents a configuration environment.
/// </summary>
internal class ConfigurationEnvironment : ObservableObject<ConfigurationEnvironment>
{
	public static readonly PropertyInfo<long> IdProperty = RegisterProperty<long>(p => p.Id);
	public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name);
	public static readonly PropertyInfo<string> SecretProperty = RegisterProperty<string>(p => p.Secret);

	/// <summary>
	/// Gets the unique identifier of the configuration environment.
	/// </summary>
	public long Id
	{
		get => ReadProperty(IdProperty);
		private set => LoadProperty(IdProperty, value);
	}

	/// <summary>
	/// Gets the name of the configuration environment.
	/// </summary>
	public string Name
	{
		get => GetProperty(NameProperty);
		private set => SetProperty(NameProperty, value);
	}

	/// <summary>
	/// Gets the secret associated with the configuration environment.
	/// </summary>
	public string Secret
	{
		get => GetProperty(SecretProperty);
		private set => SetProperty(SecretProperty, value);
	}

	[FactoryCreate]
	private async Task CreateAsync(string name, string secret, CancellationToken cancellationToken = default)
	{
		Name = name;
		Secret = secret;
		await Task.CompletedTask;
	}

	[FactoryFetch]
	private async Task FetchAsync(ConfigurationEnvironmentData data, CancellationToken cancellationToken = default)
	{
		LoadProperty(IdProperty, data.Id);
		LoadProperty(NameProperty, data.Name);
		LoadProperty(SecretProperty, data.Secret);
		await Task.CompletedTask;
	}
}