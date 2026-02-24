using Nerosoft.Euonia.Osba;

namespace Nerosoft.Starfish.Domain.Aggregates;

internal class ConfigurationPermission : ObservableObject<ConfigurationPermission>
{
	public static readonly PropertyInfo<string> UserIdProperty = RegisterProperty<string>(p => p.UserId);
	public static readonly PropertyInfo<bool> AllowReadProperty = RegisterProperty<bool>(p => p.AllowRead);
	public static readonly PropertyInfo<bool> AllowWriteProperty = RegisterProperty<bool>(p => p.AllowWrite);
	public static readonly PropertyInfo<bool> AllowPublishProperty = RegisterProperty<bool>(p => p.AllowPublish);

	public string UserId
	{
		get => GetProperty(UserIdProperty);
		private set => SetProperty(UserIdProperty, value);
	}

	public bool AllowRead
	{
		get => GetProperty(AllowReadProperty);
		set => SetProperty(AllowReadProperty, value);
	}

	public bool AllowWrite
	{
		get => GetProperty(AllowWriteProperty);
		set => SetProperty(AllowWriteProperty, value);
	}

	public bool AllowPublish
	{
		get => GetProperty(AllowPublishProperty);
		set => SetProperty(AllowPublishProperty, value);
	}

	[FactoryCreate]
	private void Create(string userId)
	{
		UserId = userId;
	}
}