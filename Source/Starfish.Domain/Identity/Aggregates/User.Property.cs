using System.Collections.ObjectModel;
using Nerosoft.Euonia.Business;

namespace Nerosoft.Starfish.Domain.Aggregates;

internal partial class User
{
	public static readonly PropertyInfo<string> UsernameProperty = RegisterProperty<string>(p => p.Username);
	public static readonly PropertyInfo<string> NicknameProperty = RegisterProperty<string>(p => p.Nickname);
	public static readonly PropertyInfo<string> PasswordProperty = RegisterProperty<string>(p => p.Password);
	public static readonly PropertyInfo<string> EmailProperty = RegisterProperty<string>(p => p.Email);
	public static readonly PropertyInfo<string> PhoneProperty = RegisterProperty<string>(p => p.Phone);
	public static readonly PropertyInfo<int> AccessFailedCountProperty = RegisterProperty<int>(p => p.AccessFailedCount);
	public static readonly PropertyInfo<DateTime?> PasswordChangedAtProperty = RegisterProperty<DateTime?>(p => p.PasswordChangedAt);
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
	public ObservableCollection<string> Roles => GetProperty(RolesProperty);
}