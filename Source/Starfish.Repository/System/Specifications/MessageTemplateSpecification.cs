using Nerosoft.Euonia.Linq;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Specifications;

internal static class MessageTemplateSpecification
{
	public static ISpecification<MessageTemplate> True()
	{
		return new DirectSpecification<MessageTemplate>(t => t.Id > 0);
	}

	public static ISpecification<MessageTemplate> IdEquals(long id)
	{
		return new DirectSpecification<MessageTemplate>(t => t.Id == id);
	}

	public static ISpecification<MessageTemplate> IdNotEquals(long id)
	{
		if (id <= 0)
		{
			return new DirectSpecification<MessageTemplate>(t => t.Id > 0);
		}
		else
		{
			return new DirectSpecification<MessageTemplate>(t => t.Id != id);
		}
	}

	public static ISpecification<MessageTemplate> NameEquals(string name)
	{
		return new DirectSpecification<MessageTemplate>(t => t.Name == name);
	}

	public static ISpecification<MessageTemplate> NameContains(string name)
	{
		return new DirectSpecification<MessageTemplate>(t => t.Name.Contains(name));
	}

	public static ISpecification<MessageTemplate> CodeEquals(string code)
	{
		code = code.Normalize(TextCaseType.Lower);
		return new DirectSpecification<MessageTemplate>(t => t.Code == code);
	}

	public static ISpecification<MessageTemplate> CodeContains(string code)
	{
		code = code.Normalize(TextCaseType.Lower);
		return new DirectSpecification<MessageTemplate>(t => t.Code.Contains(code));
	}

	public static ISpecification<MessageTemplate> LanguageEquals(string language)
	{
		return new DirectSpecification<MessageTemplate>(t => t.Language == language);
	}

	public static ISpecification<MessageTemplate> LanguageEqualsOrDefault(string language)
	{
		return new DirectSpecification<MessageTemplate>(t => t.Language == language || t.Default);
	}

	public static ISpecification<MessageTemplate> TypeEquals(TemplateType type)
	{
		return new DirectSpecification<MessageTemplate>(t => t.Type == type);
	}

	public static ISpecification<MessageTemplate> SubjectContains(string subject)
	{
		return new DirectSpecification<MessageTemplate>(t => t.Subject.Contains(subject));
	}

	public static ISpecification<MessageTemplate> IsDefault(bool @default)
	{
		return new DirectSpecification<MessageTemplate>(t => t.Default == @default);
	}

	public static ISpecification<MessageTemplate> Matches(string code, TemplateType type, string language)
	{
		ISpecification<MessageTemplate>[] specifications =
		[
			CodeEquals(code),
			LanguageEqualsOrDefault(language),
			TypeEquals(type)
		];

		return new CompositeSpecification<MessageTemplate>(PredicateOperator.AndAlso, specifications);
	}

	public static ISpecification<MessageTemplate> ExistsDefault(string code, TemplateType type, long excludeId)
	{
		ISpecification<MessageTemplate>[] specifications =
		[
			CodeEquals(code),
			TypeEquals(type),
			IsDefault(true),
			IdNotEquals(excludeId)
		];

		return new CompositeSpecification<MessageTemplate>(PredicateOperator.AndAlso, specifications);
	}
}