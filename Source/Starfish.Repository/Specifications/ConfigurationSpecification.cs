using Nerosoft.Euonia.Linq;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Specifications;

internal static class ConfigurationSpecification
{
	public static Specification<Configuration> All => new DirectSpecification<Configuration>(t => t.Id > 0);

	public static Specification<Configuration> IdEquals(long id) => new DirectSpecification<Configuration>(x => x.Id == id);

	public static Specification<Configuration> CodeEquals(string code)
	{
		code = code.Normalize(TextCaseType.Lower);
		return new DirectSpecification<Configuration>(x => x.Code == code);
	}

	public static Specification<Configuration> CodeContains(string code)
	{
		code = code.Normalize(TextCaseType.Lower);
		return new DirectSpecification<Configuration>(x => x.Code.Contains(code));
	}

	public static Specification<Configuration> NameContains(string name) => new DirectSpecification<Configuration>(x => x.Name.Contains(name));

	public static Specification<Configuration> TeamIdEquals(long teamId) => new DirectSpecification<Configuration>(x => x.TeamId == teamId);

	public static Specification<Configuration> ContainsKeyword(string keyword)
	{
		keyword = keyword.Normalize(TextCaseType.Lower);
		return new DirectSpecification<Configuration>(x => x.Code.Contains(keyword) || x.Name.Contains(keyword) || x.Description.Contains(keyword));
	}
}