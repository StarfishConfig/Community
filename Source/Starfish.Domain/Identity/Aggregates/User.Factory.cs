using Nerosoft.Euonia.Business;
using Nerosoft.Starfish.Persistent;
using Nerosoft.Starfish.Domain.Repositories;

namespace Nerosoft.Starfish.Domain.Aggregates;

internal partial class User
{
	[FactoryCreate]
	private async Task CreateAsync(string username, CancellationToken cancellationToken = default)
	{
		Username = username;
		await Task.CompletedTask;
	}

	[FactoryFetch]
	private async Task FetchAsync(string id, CancellationToken cancellationToken = default)
	{
		var repository = BusinessContext.GetRequiredService<IUserRepository>();
		var data = await repository.GetAsync(id, cancellationToken);

		if (data == null)
		{
			throw new NotFoundException(IdentityResources.IDS_ERROR_USER_NOT_FOUND);
		}

		LoadProperty(UsernameProperty, data.Username);
		LoadProperty(NicknameProperty, data.Nickname);
		LoadProperty(EmailProperty, data.Email);
		LoadProperty(PhoneProperty, data.Phone);
		LoadProperty(AccessFailedCountProperty, data.AccessFailedCount);
		LoadProperty(PasswordChangedAtProperty, data.PasswordChangedAt);
		LoadProperty(LockoutEndProperty, data.LockoutEnd);
	}

	[FactoryInsert]
	protected override Task InsertAsync(CancellationToken cancellationToken = default)
	{
		var repository = BusinessContext.GetRequiredService<IUserRepository>();
		var data = new UserData
		{
			Username = Username,
			Password = Password,
			Nickname = Nickname,
			Email = Email,
			Phone = Phone,
			AccessFailedCount = AccessFailedCount,
			PasswordChangedAt = PasswordChangedAt,
			LockoutEnd = LockoutEnd,
			Roles = [.. Roles]
		};
		return repository.SaveAsync(data, cancellationToken);
	}

	[FactoryUpdate]
	protected override Task UpdateAsync(CancellationToken cancellationToken = default)
	{
		var repository = BusinessContext.GetRequiredService<IUserRepository>();
		var data = new UserData(Id)
		{
			Username = Username,
			Password = Password,
			Nickname = Nickname,
			Email = Email,
			Phone = Phone,
			AccessFailedCount = AccessFailedCount,
			PasswordChangedAt = PasswordChangedAt,
			LockoutEnd = LockoutEnd,
			Roles = [.. Roles]
		};
		return repository.SaveAsync(data, cancellationToken);
	}
}