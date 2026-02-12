using Nerosoft.Euonia.Osba;

namespace Nerosoft.Starfish.Domain.Aggregates;

internal class ConfigurationPermission : ObservableObject<ConfigurationPermission>
{
	public static readonly PropertyInfo<string> UserIdProperty = RegisterProperty<string>(p => p.UserId);

	public string UserId
	{
		get => GetProperty(UserIdProperty);
		private set => SetProperty(UserIdProperty, value);
	}

	public static readonly PropertyInfo<bool> AllowReadProperty = RegisterProperty<bool>(p => p.AllowRead);

	public bool AllowRead
	{
		get => GetProperty(AllowReadProperty);
		set => SetProperty(AllowReadProperty, value);
	}

	public static readonly PropertyInfo<bool> AllowWriteProperty = RegisterProperty<bool>(p => p.AllowWrite);

	public bool AllowWrite
	{
		get => GetProperty(AllowWriteProperty);
		set => SetProperty(AllowWriteProperty, value);
	}

	public static readonly PropertyInfo<bool> AllowPublishProperty = RegisterProperty<bool>(p => p.AllowPublish);

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