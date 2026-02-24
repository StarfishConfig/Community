using System.Collections.ObjectModel;
using Nerosoft.Euonia.Osba;
using Nerosoft.Starfish.Domain.Events;
using Nerosoft.Starfish.Domain.Repositories;
using Nerosoft.Starfish.Domain.Rules;
using Nerosoft.Starfish.Persistent;

namespace Nerosoft.Starfish.Domain.Aggregates;

/// <summary>
/// Represents a user aggregate in the identity domain.
/// </summary>
internal sealed class User : EditableObjectBase<User, string>
{
	#region Variables
	private readonly ObservableCollection<string> _roles = [];
	#endregion

	#region Properties

	public static readonly PropertyInfo<string> UsernameProperty = RegisterProperty<string>(p => p.Username);
	public static readonly PropertyInfo<string> NicknameProperty = RegisterProperty<string>(p => p.Nickname);
	public static readonly PropertyInfo<string> PasswordProperty = RegisterProperty<string>(p => p.Password);
	public static readonly PropertyInfo<string> EmailProperty = RegisterProperty<string>(p => p.Email);
	public static readonly PropertyInfo<string> PhoneProperty = RegisterProperty<string>(p => p.Phone);
	public static readonly PropertyInfo<int> AccessFailedCountProperty = RegisterProperty<int>(p => p.AccessFailedCount);
	public static readonly PropertyInfo<DateTime?> PasswordChangedAtProperty = RegisterProperty<DateTime?>(p => p.PasswordChangedAt);
	public static readonly PropertyInfo<DateTime?> LockoutEndProperty = RegisterProperty<DateTime?>(p => p.LockoutEnd);

	/// <summary>
	/// Gets or sets the username.
	/// </summary>
	public string Username
	{
		get => GetProperty(UsernameProperty);
		private set => SetProperty(UsernameProperty, value);
	}

	/// <summary>
	/// Gets or sets the nickname.
	/// </summary>
	public string Nickname
	{
		get => GetProperty(NicknameProperty);
		private set => SetProperty(NicknameProperty, value);
	}

	/// <summary>
	/// Gets or sets the password.
	/// </summary>
	public string Password
	{
		get => GetProperty(PasswordProperty);
		private set => SetProperty(PasswordProperty, value);
	}

	/// <summary>
	/// Gets or sets the email address.
	/// </summary>
	public string Email
	{
		get => GetProperty(EmailProperty);
		private set => SetProperty(EmailProperty, value);
	}

	/// <summary>
	/// Gets or sets the phone number.
	/// </summary>
	public string Phone
	{
		get => GetProperty(PhoneProperty);
		private set => SetProperty(PhoneProperty, value);
	}

	/// <summary>
	/// Gets or sets the count of failed access attempts.
	/// </summary>
	public int AccessFailedCount
	{
		get => GetProperty(AccessFailedCountProperty);
		private set => SetProperty(AccessFailedCountProperty, value);
	}

	/// <summary>
	/// Gets or sets the time when the password was last changed.
	/// </summary>
	public DateTime? PasswordChangedAt
	{
		get => GetProperty(PasswordChangedAtProperty);
		private set => SetProperty(PasswordChangedAtProperty, value);
	}

	/// <summary>
	/// Gets or sets the lockout end time.
	/// </summary>
	public DateTime? LockoutEnd
	{
		get => GetProperty(LockoutEndProperty);
		private set => SetProperty(LockoutEndProperty, value);
	}

	/// <summary>
	/// Gets or sets the roles associated with the user.
	/// </summary>
	public IReadOnlyCollection<string> Roles => _roles;
	#endregion

	#region Business Methods

	/// <summary>
	/// Sets the password for the user.
	/// </summary>
	/// <param name="newPassword">The new password.</param>
	/// <param name="actionType">The type of action triggering the password change.</param>
	internal void SetPassword(string newPassword, string actionType = null)
	{
		Password = newPassword;
		PasswordChangedAt = DateTime.Now;
		if (!string.IsNullOrWhiteSpace(actionType))
		{
			RaiseEvent(new UserPasswordChangedEvent(Id, actionType, PasswordChangedAt.Value));
		}
	}

	/// <summary>
	/// Sets the email address for the user.
	/// </summary>
	/// <param name="email">The email address to set.</param>
	internal void SetEmail(string email)
	{
		Email = email.Normalize(TextCaseType.Lower);
	}

	/// <summary>
	/// Sets the phone number for the user.
	/// </summary>
	/// <param name="phone">The phone number to set.</param>
	internal void SetPhone(string phone)
	{
		Phone = phone;
	}

	/// <summary>
	/// Sets the nickname for the user.
	/// </summary>
	/// <param name="nickname">The nickname to set.</param>
	internal void SetNickname(string nickname)
	{
		Nickname = nickname;
	}

	/// <summary>
	/// Increases the count of failed access attempts.
	/// </summary>
	internal void IncreaseAccessFailedCount()
	{
		if (LockoutEnd >= DateTime.UtcNow)
		{
			return;
		}

		AccessFailedCount++;
		if (AccessFailedCount >= 10)
		{
			LockoutEnd = DateTime.UtcNow.AddMinutes(30);
		}
	}

	/// <summary>
	/// Resets the count of failed access attempts for the user.
	/// </summary>
	/// <param name="forceReset">
	/// A boolean value indicating whether to forcefully reset the <see cref="LockoutEnd"/> 
	/// and <see cref="AccessFailedCount"/>. If <c>true</c>, the values will be reset 
	/// regardless of the current lockout state. If <c>false</c>, the reset will only occur 
	/// if the lockout period has expired.
	/// </param>
	internal void ResetAccessFailedCount(bool forceReset = false)
	{
		if (!forceReset && LockoutEnd >= DateTime.UtcNow)
		{
			return;
		}

		AccessFailedCount = 0;
		LockoutEnd = null;
	}

	/// <summary>
	/// Sets the roles for the user.
	/// </summary>
	/// <param name="roles">The roles to set.</param>
	internal void AssignRoles(params string[] roles)
	{
		if (roles == null || roles.Length == 0)
		{
			return;
		}

		_roles.RemoveAll(t => !roles.Contains(t, StringComparer.OrdinalIgnoreCase));

		foreach (var role in roles)
		{
			var name = role.Normalize(TextCaseType.Lower);

			if (Roles.Contains(name, StringComparer.OrdinalIgnoreCase))
			{
				continue;
			}

			_roles.Add(role);
		}
	}

	/// <summary>
	/// Sets the lockout end time for the user.
	/// </summary>
	/// <param name="until">The lockout end time.</param>
	internal void SetLockoutEnd(DateTime? until)
	{
		LockoutEnd = until;
	}

	protected override void AddRules()
	{
		Rules.AddRule(new UsernameCheckRule(UsernameProperty));
		Rules.AddRule(new PasswordStrengthRule(PasswordProperty));
		Rules.AddRule(new EmailAddressCheckRule(EmailProperty));
		Rules.AddRule(new PhoneNumberCheckRule(PhoneProperty));
	}

	#endregion

	#region Factory Methods
	[FactoryCreate]
	private async Task CreateAsync(string username, CancellationToken cancellationToken = default)
	{
		Username = username;
		await Task.CompletedTask;
	}

	[FactoryFetch]
	protected override async Task FetchAsync(string id, CancellationToken cancellationToken = default)
	{
		var repository = BusinessContext.GetRequiredService<IUserRepository>();
		var data = await repository.GetAsync(id, cancellationToken);

		if (data == null)
		{
			throw new NotFoundException(IdentityResources.IDS_ERROR_USER_NOT_FOUND);
		}

		LoadProperty(IdProperty, id);
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
	#endregion
}