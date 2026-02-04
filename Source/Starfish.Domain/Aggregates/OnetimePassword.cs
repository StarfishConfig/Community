using Nerosoft.Euonia.Osba;
using Nerosoft.Starfish.Domain.Commands;
using Nerosoft.Starfish.Persistent.Repositories;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Domain.Aggregates;

internal class OnetimePassword : EditableObjectBase<OnetimePassword, long>
{
	#region Properties
	public static readonly PropertyInfo<string> RequestIdProperty = RegisterProperty<string>(p => p.RequestId);
	public static readonly PropertyInfo<string> CodeProperty = RegisterProperty<string>(p => p.Code);
	public static readonly PropertyInfo<string> RecipientProperty = RegisterProperty<string>(p => p.Recipient);
	public static readonly PropertyInfo<DateTime?> ExpirationProperty = RegisterProperty<DateTime?>(p => p.Expiration);
	public static readonly PropertyInfo<DateTime?> CheckedProperty = RegisterProperty<DateTime?>(p => p.Checked);
	public static readonly PropertyInfo<int?> DurationProperty = RegisterProperty<int?>(p => p.Duration);
	public static readonly PropertyInfo<OnetimePasswordUsage> UsageProperty = RegisterProperty<OnetimePasswordUsage>(p => p.Usage);

	public string RequestId
	{
		get => GetProperty(RequestIdProperty);
		private set => SetProperty(RequestIdProperty, value);
	}

	public string Code
	{
		get => GetProperty(CodeProperty);
		private set => SetProperty(CodeProperty, value);
	}

	public string Recipient
	{
		get => GetProperty(RecipientProperty);
		private set => SetProperty(RecipientProperty, value);
	}

	public DateTime? Expiration
	{
		get => GetProperty(ExpirationProperty);
		private set => SetProperty(ExpirationProperty, value);
	}

	public DateTime? Checked
	{
		get => GetProperty(CheckedProperty);
		private set => SetProperty(CheckedProperty, value);
	}

	public int? Duration
	{
		get => GetProperty(DurationProperty);
		private set => SetProperty(DurationProperty, value);
	}

	public OnetimePasswordUsage Usage
	{
		get => GetProperty(UsageProperty);
		private set => SetProperty(UsageProperty, value);
	}

	#endregion
	#region Business Methods & Rules
	public void CheckOff(DateTime time)
	{
		Checked = time;
	}
	#endregion

	#region Factory Methods
	[FactoryCreate]
	private async Task CreateAsync(OnetimePasswordCreateCommand command, CancellationToken cancellationToken = default)
	{
		RequestId = command.RequestId;
		Code = command.Code;
		Recipient = command.Recipient;
		Usage = command.Usage;
		if (Duration.HasValue)
		{
			Expiration = DateTime.UtcNow.Add(command.Duration.Value);
			Duration = (int)command.Duration.Value.TotalSeconds;
		}
		else
		{
			Expiration = null;
			Duration = null;
		}
		await Task.CompletedTask;
	}

	[FactoryFetch]
	private async Task FetchAsync(string requestId, CancellationToken cancellationToken = default)
	{
		var repository = BusinessContext.GetRequiredService<IOnetimePasswordRepository>();
		var data = await repository.GetAsync(requestId, cancellationToken);
		if (data != null)
		{
			throw new NotFoundException();
		}

		LoadProperty(IdProperty, data.Id);
		LoadProperty(RequestIdProperty, data.RequestId);
		LoadProperty(CodeProperty, data.Code);
		LoadProperty(RecipientProperty, data.Recipient);
		LoadProperty(ExpirationProperty, data.Expiration);
		LoadProperty(CheckedProperty, data.Checked);
		LoadProperty(DurationProperty, data.Duration);
		LoadProperty(UsageProperty, data.Usage);
	}

	[FactoryInsert]
	protected override Task InsertAsync(CancellationToken cancellationToken = default)
	{
		return base.InsertAsync(cancellationToken);
	}

	[FactoryUpdate]
	protected override Task UpdateAsync(CancellationToken cancellationToken = default)
	{
		var repository = BusinessContext.GetRequiredService<IOnetimePasswordRepository>();

		return repository.CheckOffAsync(RequestId, Checked ?? DateTime.UtcNow, cancellationToken);
	}
	#endregion
}
