using Nerosoft.Euonia.Linq;
using Nerosoft.Starfish.Persist.Entities;

namespace Nerosoft.Starfish.Persist.Specifications;

internal static class UserSpecification
{
	public static ISpecification<UserEntity> UsernameEquals(string username)
	{
		username = username.Normalize(TextCaseType.Lower);
		return new DirectSpecification<UserEntity>(x => x.Username == username);
	}

	public static ISpecification<UserEntity> EmailEquals(string email)
	{
		email = email.Normalize(TextCaseType.Lower);
		return new DirectSpecification<UserEntity>(x => x.Email == email);
	}

	public static ISpecification<UserEntity> PhoneEquals(string phone)
	{
		phone = phone.Normalize(TextCaseType.Lower);
		return new DirectSpecification<UserEntity>(x => x.Phone == phone);
	}
}