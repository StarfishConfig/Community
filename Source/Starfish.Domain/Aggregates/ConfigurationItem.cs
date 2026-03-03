using Nerosoft.Euonia.Domain;
using Nerosoft.Euonia.Osba;

namespace Nerosoft.Starfish.Domain.Aggregates;

/// <summary>
/// Represents a configuration item.
/// </summary>
internal class ConfigurationItem : EditableObjectBase<ConfigurationItem, long>
{
	#region Properties

	public static readonly PropertyInfo<string> KeyProperty = RegisterProperty<string>(p => p.Key);

	public string Key
	{
		get => GetProperty(KeyProperty);
		private set => SetProperty(KeyProperty, value);
	}

	public static readonly PropertyInfo<string> ValueProperty = RegisterProperty<string>(p => p.Value);

	public string Value
	{
		get => GetProperty(ValueProperty);
		private set => SetProperty(ValueProperty, value);
	}

	#endregion
}