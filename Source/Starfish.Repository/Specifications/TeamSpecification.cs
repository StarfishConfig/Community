using Nerosoft.Euonia.Linq;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Specifications;

internal static class TeamSpecification
{
	public static Specification<Team> All => new TrueSpecification<Team>();

	public static Specification<Team> IdEquals(long id)
	{
		return new DirectSpecification<Team>(t => t.Id == id);
	}

	public static Specification<Team> NameEquals(string name)
	{
		name = name.Normalize(TextCaseType.Lower);

		return new DirectSpecification<Team>(t => t.Name.ToLower() == name);
	}

	public static Specification<Team> NameContains(string name)
	{
		name = name.Normalize(TextCaseType.Lower);

		return new DirectSpecification<Team>(t => t.Name.ToLower().Contains(name));
	}

	public static Specification<Team> DescriptionContains(string description)
	{
		description = description.Normalize(TextCaseType.Lower);

		return new DirectSpecification<Team>(t => t.Description.ToLower().Contains(description));
	}

	public static Specification<Team> ContainsKeyword(string keyword)
	{
		ISpecification<Team>[] specifications =
		[
			NameContains(keyword),
			DescriptionContains(keyword)
		];

		return new CompositeSpecification<Team>(PredicateOperator.AndAlso, specifications);
	}

	public static Specification<Team> Joined(string userId)
	{
		return new DirectSpecification<Team>(t => t.Members.Any(m => m.UserId == userId));
	}

	public static Specification<Team> Owned(string userId)
	{
		return new DirectSpecification<Team>(t => t.OwnerId == userId);
	}

	// public static ISpecification<Team> WithType(int type)
	// {
	// 	return type switch
	// 	{
	// 		1 => new DirectSpecification<Team>(t => t.MemberCount >= 100),
	// 		2 => new DirectSpecification<Team>(t => t.MemberCount >= 50 && t.MemberCount < 100),
	// 		3 => new DirectSpecification<Team>(t => t.MemberCount >= 10 && t.MemberCount < 50),
	// 		4 => new DirectSpecification<Team>(t => t.MemberCount < 10),
	// 		_ => All,
	// 	};
	// }
}