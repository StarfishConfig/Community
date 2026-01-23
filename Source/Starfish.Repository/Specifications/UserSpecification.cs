using Nerosoft.Euonia.Linq;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Specifications;

internal static class UserSpecification
{
	public static ISpecification<User> True()
	{
		return new DirectSpecification<User>(x => x.IsDeleted == false);
	}

	public static ISpecification<User> IdEquals(string id)
	{
		return new DirectSpecification<User>(x => x.Id == id);
	}

	public static ISpecification<User> UsernameEquals(string username)
	{
		username = username.Normalize(TextCaseType.Lower);
		return new DirectSpecification<User>(x => x.Username == username);
	}

	public static ISpecification<User> EmailEquals(string email)
	{
		email = email.Normalize(TextCaseType.Lower);
		return new DirectSpecification<User>(x => x.Email == email);
	}

	public static ISpecification<User> PhoneEquals(string phone)
	{
		phone = phone.Normalize(TextCaseType.Lower);
		return new DirectSpecification<User>(x => x.Phone == phone);
	}

	public static ISpecification<User> ContainsKeyword(string keyword)
	{
		keyword = keyword.Normalize(TextCaseType.Lower);
		return new DirectSpecification<User>(x =>
			x.Username.Contains(keyword) ||
			x.Nickname.Contains(keyword) ||
			x.Email.Contains(keyword) ||
			x.Phone.Contains(keyword));
	}

	public static ISpecification<User> IsLocked(bool locked)
	{
		if (locked)
		{
			return new DirectSpecification<User>(x =>
				x.LockoutEnd != null && x.LockoutEnd > DateTime.UtcNow);
		}
		else
		{
			return new DirectSpecification<User>(x =>
				x.LockoutEnd == null || x.LockoutEnd <= DateTime.UtcNow);
		}
	}

	public static Specification<User> HasRole(string name)
	{
		return new DirectSpecification<User>(user => user.Roles.Any(role => role.Name == name));
	}

	public static ISpecification<User> AuthorityEquals(string provider, string openId)
	{
		return new DirectSpecification<User>(t => t.Authorities.Any(a => a.Provider == provider && a.OpenId == openId));
	}
}