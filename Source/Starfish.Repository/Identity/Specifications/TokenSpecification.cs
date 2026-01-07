using Nerosoft.Euonia.Linq;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Specifications;

internal static class TokenSpecification
{
	public static ISpecification<Token> TypeEquals(string type)
	{
		type = type.Normalize(TextCaseType.Lower);
		return new DirectSpecification<Token>(t => t.Type == type);
	}

	public static ISpecification<Token> KeyEquals(string key)
	{
		return new DirectSpecification<Token>(t => t.Key == key);
	}
}