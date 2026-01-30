using Nerosoft.Euonia.Linq;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Specifications;

internal static class TemplateSpecification
{
	public static ISpecification<Template> True()
	{
		return new DirectSpecification<Template>(t => t.Id > 0);
	}

	public static ISpecification<Template> IdEquals(long id)
	{
		return new DirectSpecification<Template>(t => t.Id == id);
	}

	public static ISpecification<Template> IdNotEquals(long id)
	{
		if (id <= 0)
		{
			return new DirectSpecification<Template>(t => t.Id > 0);
		}
		else
		{
			return new DirectSpecification<Template>(t => t.Id != id);
		}
	}

	public static ISpecification<Template> NameEquals(string name)
	{
		return new DirectSpecification<Template>(t => t.Name == name);
	}

	public static ISpecification<Template> NameContains(string name)
	{
		return new DirectSpecification<Template>(t => t.Name.Contains(name));
	}

	public static ISpecification<Template> CodeEquals(string code)
	{
		code = code.Normalize(TextCaseType.Lower);
		return new DirectSpecification<Template>(t => t.Code == code);
	}

	public static ISpecification<Template> CodeContains(string code)
	{
		code = code.Normalize(TextCaseType.Lower);
		return new DirectSpecification<Template>(t => t.Code.Contains(code));
	}

	public static ISpecification<Template> LanguageEquals(string language)
	{
		return new DirectSpecification<Template>(t => t.Language == language);
	}

	public static ISpecification<Template> LanguageEqualsOrDefault(string language)
	{
		return new DirectSpecification<Template>(t => t.Language == language || t.Default);
	}

	public static ISpecification<Template> TypeEquals(TemplateType type)
	{
		return new DirectSpecification<Template>(t => t.Type == type);
	}

	public static ISpecification<Template> SubjectContains(string subject)
	{
		return new DirectSpecification<Template>(t => t.Subject.Contains(subject));
	}

	public static ISpecification<Template> IsDefault(bool @default)
	{
		return new DirectSpecification<Template>(t => t.Default == @default);
	}

	public static ISpecification<Template> Matches(string code, TemplateType type, string language)
	{
		ISpecification<Template>[] specifications =
		[
			CodeEquals(code),
			LanguageEqualsOrDefault(language),
			TypeEquals(type)
		];

		return new CompositeSpecification<Template>(PredicateOperator.AndAlso, specifications);
	}

	public static ISpecification<Template> ExistsDefault(string code, TemplateType type, long excludeId)
	{
		ISpecification<Template>[] specifications =
		[
			CodeEquals(code),
			TypeEquals(type),
			IsDefault(true),
			IdNotEquals(excludeId)
		];

		return new CompositeSpecification<Template>(PredicateOperator.AndAlso, specifications);
	}

	public static ISpecification<Template> ContainsKeyword(string keyword)
	{
		return new DirectSpecification<Template>(t => t.Name.Contains(keyword) || t.Code.Contains(keyword) || t.Subject.Contains(keyword) || t.Body.Contains(keyword));
	}
}