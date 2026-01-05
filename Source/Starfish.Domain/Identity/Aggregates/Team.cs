using Nerosoft.Euonia.Business;

namespace Nerosoft.Starfish.Domain.Aggregates;

internal sealed partial class Team : EditableObjectBase<Team, string>
{
	public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name);

	public string Name
	{
		get => GetProperty(NameProperty);
		private set => SetProperty(NameProperty, value);
	}
}