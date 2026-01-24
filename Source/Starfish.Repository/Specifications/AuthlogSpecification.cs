using Nerosoft.Euonia.Linq;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Specifications;

internal static class AuthlogSpecification
{
	public static Specification<Authlog> All => new DirectSpecification<Authlog>(t => t.Id > 0);

	public static Specification<Authlog> UserIdEquals(string userId)
	{
		return new DirectSpecification<Authlog>(t => t.UserId == userId);
	}

	public static Specification<Authlog> UsernameEquals(string username)
	{
		username = username.Normalize(TextCaseType.Lower);
		return new DirectSpecification<Authlog>(t => t.Username == username);
	}

	public static Specification<Authlog> SourceEquals(string source)
	{
		source = source.Normalize(TextCaseType.Lower);
		return new DirectSpecification<Authlog>(t => t.Source == source);
	}

	public static Specification<Authlog> GrantTypeEquals(string grantType)
	{
		grantType = grantType.Normalize(TextCaseType.Lower);
		return new DirectSpecification<Authlog>(t => t.GrantType == grantType);
	}

	public static Specification<Authlog> After(DateTime time)
	{
		time = time.Date;
		return new DirectSpecification<Authlog>(t => t.Timestamp >= time);
	}

	public static Specification<Authlog> Before(DateTime time)
	{
		time = time.AddDays(1).Date;
		return new DirectSpecification<Authlog>(t => t.Timestamp < time);
	}

	//public static Specification<Authlog> SuccessEquals(bool success)
	//{
	//	return new DirectSpecification<Authlog>(t => t.Success == success);
	//}

	//public static Specification<Authlog> FailureEquals(bool failure)
	//{
	//	return new DirectSpecification<Authlog>(t => t.Success != failure);
	//}

	public static Specification<Authlog> ResultEquals(bool result)
	{
		if (result)
		{
			return new DirectSpecification<Authlog>(t => t.Success == true);
		}
		else
		{
			return new DirectSpecification<Authlog>(t => t.Success == false);
		}
	}
}
