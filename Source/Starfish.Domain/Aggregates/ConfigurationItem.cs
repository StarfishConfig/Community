using Nerosoft.Euonia.Osba;

namespace Nerosoft.Starfish.Domain.Aggregates;

/// <summary>
/// Represents a configuration item.
/// </summary>
internal class ConfigurationItem : EditableObjectBase<ConfigurationItem, long>
{
	#region Properties

	public static readonly PropertyInfo<long> ConfigurationIdProperty = RegisterProperty<long>(p => p.ConfigurationId);
	public static readonly PropertyInfo<string> KeyProperty = RegisterProperty<string>(p => p.Key);
	public static readonly PropertyInfo<string> ValueProperty = RegisterProperty<string>(p => p.Value);

	/// <summary>
	/// Gets the unique identifier for the current configuration instance.
	/// </summary>
	public long ConfigurationId
	{
		get => GetProperty(ConfigurationIdProperty);
		private set => SetProperty(ConfigurationIdProperty, value);
	}

	/// <summary>
	/// Gets the unique identifier key associated with this instance.
	/// </summary>
	public string Key
	{
		get => GetProperty(KeyProperty);
		private set => SetProperty(KeyProperty, value);
	}

	/// <summary>
	/// Gets the current value associated with this property.
	/// </summary>
	public string Value
	{
		get => GetProperty(ValueProperty);
		private set => SetProperty(ValueProperty, value);
	}

	#endregion

	#region Business Methods & Rules
	protected override void AddRules()
	{
		Rules.AddRule<ConfigurationItem>(KeyProperty, target => Task.FromResult(!string.IsNullOrWhiteSpace(Key)), "Key is required.");
	}
	#endregion

	#region Factory Methods

	[FactoryCreate]
	protected override Task CreateAsync(CancellationToken cancellationToken = default)
	{
		return Task.CompletedTask;
	}

	[FactoryFetch]
	protected override async Task FetchAsync(long id, CancellationToken cancellationToken = default)
	{
	}

	[FactoryInsert]
	protected override async Task InsertAsync(CancellationToken cancellationToken = default)
	{

	}

	[FactoryUpdate]
	protected override async Task UpdateAsync(CancellationToken cancellationToken = default)
	{

	}

	[FactoryDelete]
	protected override async Task DeleteAsync(CancellationToken cancellationToken = default)
	{

	}

	#endregion
}