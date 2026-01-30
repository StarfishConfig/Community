using Nerosoft.Euonia.Business;
using Nerosoft.Starfish.Domain.Commands;
using Nerosoft.Starfish.Persistent.Data;
using Nerosoft.Starfish.Persistent.Repositories;

namespace Nerosoft.Starfish.Domain.Aggregates;

internal sealed class ConfigurationChangelog : EditableObjectBase<ConfigurationChangelog, long>
{
	#region Properties

	public static readonly PropertyInfo<long> ConfigurationIdProperty = RegisterProperty<long>(p => p.ConfigurationId);
	public static readonly PropertyInfo<long> ItemIdProperty = RegisterProperty<long>(p => p.ItemId);
	public static readonly PropertyInfo<string> KeyProperty = RegisterProperty<string>(p => p.Key);
	public static readonly PropertyInfo<string> ValueProperty = RegisterProperty<string>(p => p.Value);
	public static readonly PropertyInfo<string> ChangeTypeProperty = RegisterProperty<string>(p => p.ChangeType);
	public static readonly PropertyInfo<string> ChangedByProperty = RegisterProperty<string>(p => p.ChangedBy);
	public static readonly PropertyInfo<DateTime> ChangedAtProperty = RegisterProperty<DateTime>(p => p.ChangedAt);

	public long ConfigurationId
	{
		get => GetProperty(ConfigurationIdProperty);
		private set => SetProperty(ConfigurationIdProperty, value);
	}

	public long ItemId
	{
		get => GetProperty(ItemIdProperty);
		private set => SetProperty(ItemIdProperty, value);
	}

	public string Key
	{
		get => GetProperty(KeyProperty);
		private set => SetProperty(KeyProperty, value);
	}

	public string Value
	{
		get => GetProperty(ValueProperty);
		private set => SetProperty(ValueProperty, value);
	}

	public string ChangeType
	{
		get => GetProperty(ChangeTypeProperty);
		private set => SetProperty(ChangeTypeProperty, value);
	}

	public string ChangedBy
	{
		get => GetProperty(ChangedByProperty);
		private set => SetProperty(ChangedByProperty, value);
	}

	public DateTime ChangedAt
	{
		get => GetProperty(ChangedAtProperty);
		private set => SetProperty(ChangedAtProperty, value);
	}

	#endregion

	#region Business Methods & Rules

	protected override void AddRules()
	{
		Rules.AddRule<ConfigurationChangelog>(ConfigurationIdProperty, _ => Task.FromResult(ConfigurationId > 0), "ConfigurationId must be a positive number.");
		Rules.AddRule<ConfigurationChangelog>(ItemIdProperty, _ => Task.FromResult(ItemId >= 0), "ItemId must be a positive number.");
		Rules.AddRule<ConfigurationChangelog>(KeyProperty, _ => Task.FromResult(!string.IsNullOrWhiteSpace(Key)), "Key is required.");
	}

	#endregion

	#region Factory Methods

	[FactoryCreate]
	private async Task CreateAsync(ConfigurationChangelogCreateCommand command, CancellationToken cancellationToken = default)
	{
		ConfigurationId = command.ConfigurationId;
		ItemId = command.ItemId;
		Key = command.Key;
		Value = command.Value;
		ChangeType = command.ChangeType;
		ChangedBy = command.ChangedBy;
		ChangedAt = command.ChangedAt;
		await base.CreateAsync(cancellationToken);
	}

	[FactoryInsert]
	protected override Task InsertAsync(CancellationToken cancellationToken = default)
	{
		var data = new ConfigurationChangelogData
		{
			ConfigurationId = ConfigurationId,
			ItemId = ItemId,
			Key = Key,
			Value = Value,
			ChangeType = ChangeType,
			ChangedBy = ChangedBy,
			ChangedAt = ChangedAt
		};

		var repository = BusinessContext.GetRequiredService<IConfigurationChangelogRepository>();
		return repository.SaveAsync(data, cancellationToken);
	}

	#endregion
}