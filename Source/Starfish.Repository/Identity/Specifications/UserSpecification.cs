using Nerosoft.Euonia.Linq;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Specifications;

internal static class UserSpecification
{
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
}