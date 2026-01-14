using Nerosoft.Starfish.Domain.Events;
using Nerosoft.Starfish.Domain.Rules;

namespace Nerosoft.Starfish.Domain.Aggregates;

/// <summary>
/// Represents a user aggregate in the identity domain.
/// </summary>
internal sealed partial class User : EditableObjectBase<User, string>
{
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
	internal void AssignRoles(params string[] roles)
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