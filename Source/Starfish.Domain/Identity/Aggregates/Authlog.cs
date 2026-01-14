using Nerosoft.Euonia.Business;
using Nerosoft.Starfish.Domain.Commands;
using Nerosoft.Starfish.Domain.Repositories;
using Nerosoft.Starfish.Persistent;

namespace Nerosoft.Starfish.Domain.Aggregates;

internal sealed class Authlog : EditableObjectBase<Authlog, string>
{
	public static readonly PropertyInfo<string> UserIdProperty = RegisterProperty<string>(p => p.UserId);
	public static readonly PropertyInfo<string> UsernameProperty = RegisterProperty<string>(p => p.Username);
	public static readonly PropertyInfo<string> GrantTypeProperty = RegisterProperty<string>(p => p.GrantType);
	public static readonly PropertyInfo<string> RequestIdProperty = RegisterProperty<string>(p => p.RequestId);
	public static readonly PropertyInfo<string> IpAddressProperty = RegisterProperty<string>(p => p.IpAddress);
	public static readonly PropertyInfo<string> UserAgentProperty = RegisterProperty<string>(p => p.UserAgent);
	public static readonly PropertyInfo<string> RefererProperty = RegisterProperty<string>(p => p.Referer);
	public static readonly PropertyInfo<string> AppNameProperty = RegisterProperty<string>(p => p.AppName);
	public static readonly PropertyInfo<string> AppVersionProperty = RegisterProperty<string>(p => p.AppVersion);
	public static readonly PropertyInfo<string> OsPlatformProperty = RegisterProperty<string>(p => p.OsPlatform);
	public static readonly PropertyInfo<string> SourceProperty = RegisterProperty<string>(p => p.Source);
	public static readonly PropertyInfo<bool> SuccessProperty = RegisterProperty<bool>(p => p.Success);
	public static readonly PropertyInfo<string> RemarkProperty = RegisterProperty<string>(p => p.Remark);
	public static readonly PropertyInfo<DateTime> TimestampProperty = RegisterProperty<DateTime>(p => p.Timestamp);

	public string UserId
	{
		get => GetProperty(UserIdProperty);
		private set => SetProperty(UserIdProperty, value);
	}

	public string Username
	{
		get => GetProperty(UsernameProperty);
		private set => SetProperty(UsernameProperty, value);
	}

	public string GrantType
	{
		get => GetProperty(GrantTypeProperty);
		private set => SetProperty(GrantTypeProperty, value);
	}

	public string RequestId
	{
		get => GetProperty(RequestIdProperty);
		private set => SetProperty(RequestIdProperty, value);
	}

	public string IpAddress
	{
		get => GetProperty(IpAddressProperty);
		private set => SetProperty(IpAddressProperty, value);
	}

	public string UserAgent
	{
		get => GetProperty(UserAgentProperty);
		private set => SetProperty(UserAgentProperty, value);
	}

	public string Referer
	{
		get => GetProperty(RefererProperty);
		private set => SetProperty(RefererProperty, value);
	}

	public string AppName
	{
		get => GetProperty(AppNameProperty);
		private set => SetProperty(AppNameProperty, value);
	}

	public string AppVersion
	{
		get => GetProperty(AppVersionProperty);
		private set => SetProperty(AppVersionProperty, value);
	}

	public string OsPlatform
	{
		get => GetProperty(OsPlatformProperty);
		private set => SetProperty(OsPlatformProperty, value);
	}

	public string Source
	{
		get => GetProperty(SourceProperty);
		private set => SetProperty(SourceProperty, value);
	}

	public bool Success
	{
		get => GetProperty(SuccessProperty);
		private set => SetProperty(SuccessProperty, value);
	}

	public string Remark
	{
		get => GetProperty(RemarkProperty);
		private set => SetProperty(RemarkProperty, value);
	}

	public DateTime Timestamp
	{
		get => GetProperty(TimestampProperty);
		private set => SetProperty(TimestampProperty, value);
	}

	[FactoryCreate]
	private async Task CreateAsync(AuthlogCreateCommand command, CancellationToken cancellationToken = default)
	{
		UserId = command.UserId;
		Username = command.Username;
		GrantType = command.GrantType;
		RequestId = command.RequestId;
		IpAddress = command.IpAddress;
		UserAgent = command.UserAgent;
		Referer = command.Referer;
		AppName = command.AppName;
		AppVersion = command.AppVersion;
		OsPlatform = command.OsPlatform;
		Source = command.Source;
		Success = command.Success;
		Remark = command.Remark;
		Timestamp = command.Timestamp;
		await Task.CompletedTask;
	}

	[FactoryInsert]
	protected override Task InsertAsync(CancellationToken cancellationToken = default)
	{
		var data = new AuthlogData
		{
			UserId = UserId,
			Username = Username,
			GrantType = GrantType,
			RequestId = RequestId,
			IpAddress = IpAddress,
			UserAgent = UserAgent,
			Referer = Referer,
			AppName = AppName, 
			AppVersion = AppVersion, 
			OsPlatform = OsPlatform,
			Success = Success,
			Remark = Remark,
			Timestamp = Timestamp
		};
		var repository = BusinessContext.GetService<IAuthlogRepository>();
		return repository.SaveAsync(data, cancellationToken);
	}
}