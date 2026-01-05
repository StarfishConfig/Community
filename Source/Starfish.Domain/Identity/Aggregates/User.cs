using System.Collections.ObjectModel;
using Nerosoft.Euonia.Business;
using Nerosoft.Starfish.Domain.Events;
using Nerosoft.Starfish.Domain.Rules;

namespace Nerosoft.Starfish.Domain.Aggregates;

/// <summary>
/// Represents a user aggregate in the identity domain.
/// </summary>
internal sealed partial class User : EditableObjectBase<User, string>
{
	public static readonly PropertyInfo<string> UsernameProperty = RegisterProperty<string>(p => p.Username);
	public static readonly PropertyInfo<string> NicknameProperty = RegisterProperty<string>(p => p.Nickname);
	public static readonly PropertyInfo<string> PasswordProperty = RegisterProperty<string>(p => p.Password);
	public static readonly PropertyInfo<string> EmailProperty = RegisterProperty<string>(p => p.Email);
	public static readonly PropertyInfo<string> PhoneProperty = RegisterProperty<string>(p => p.Phone);
	public static readonly PropertyInfo<int> AccessFailedCountProperty = RegisterProperty<int>(p => p.AccessFailedCount);
	public static readonly PropertyInfo<DateTime?> PasswordChangedTimeProperty = RegisterProperty<DateTime?>(p => p.PasswordChangedTime);
	public static readonly PropertyInfo<DateTime?> LockoutEndProperty = RegisterProperty<DateTime?>(p => p.LockoutEnd);
	public static readonly PropertyInfo<ObservableCollection<string>> RolesProperty = RegisterProperty<ObservableCollection<string>>(p => p.Roles, nameof(Roles), []);

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
	public DateTime? PasswordChangedTime
	{
		get => GetProperty(PasswordChangedTimeProperty);
		private set => SetProperty(PasswordChangedTimeProperty, value);
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
	public ObservableCollection<string> Roles => GetProperty(RolesProperty);

	/// <summary>
	/// Sets the password for the user.
	/// </summary>
	/// <param name="password">The new password.</param>
	/// <param name="actionType">The type of action triggering the password change.</param>
	internal void SetPassword(string password, string actionType = null)
	{
		// var salt = RandomUtility.GenerateRandomString();
		// var hash = Cryptography.DES.Encrypt(password, Encoding.UTF8.GetBytes(salt));
		// PasswordHash = hash;
		// PasswordSalt = salt;
		Password = password;
		PasswordChangedTime = DateTime.Now;
		if (!string.IsNullOrWhiteSpace(actionType))
		{
			RaiseEvent(new UserPasswordChangedEvent(Id, actionType, PasswordChangedTime.Value));
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
		AccessFailedCount++;
		if (AccessFailedCount >= 10)
		{
			LockoutEnd = DateTime.Now.AddMinutes(30);
		}
	}

	/// <summary>
	/// Resets the count of failed access attempts.
	/// </summary>
	internal void ResetAccessFailedCount()
	{
		AccessFailedCount = 0;
		LockoutEnd = null;
	}

	/// <summary>
	/// Sets the roles for the user.
	/// </summary>
	/// <param name="roles">The roles to set.</param>
	internal void SetRoles(params string[] roles)
	{
		if (roles?.Any() != true)
		{
			return;
		}

		Roles.RemoveAll(t => !roles.Contains(t, StringComparer.OrdinalIgnoreCase));

		foreach (var role in roles)
		{
			var name = role.Normalize(TextCaseType.Lower);

			if (Roles.Contains(name, StringComparer.OrdinalIgnoreCase))
			{
				continue;
			}

			Roles.Add(role);
		}

		OnPropertyChanged(RolesProperty);
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
		Rules.AddRule(new UserRoleCheckRule(RolesProperty));
	}
}