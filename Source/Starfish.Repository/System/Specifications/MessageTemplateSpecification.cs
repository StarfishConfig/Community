using Nerosoft.Euonia.Linq;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Specifications;

internal static class MessageTemplateSpecification
{
	public static ISpecification<MessageTemplate> IdEquals(string id)
	{
		return new DirectSpecification<MessageTemplate>(t => t.Id == id);
	}

	public static ISpecification<MessageTemplate> IdNotEquals(string id)
	{
		if (string.IsNullOrEmpty(id))
		{
			return new DirectSpecification<MessageTemplate>(t => t.Id != null);
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

	public static ISpecification<MessageTemplate> CodeEquals(string code)
	{
		code = code.Normalize(TextCaseType.Lower);
		return new DirectSpecification<MessageTemplate>(t => t.Code == code);
	}

	public static ISpecification<MessageTemplate> LanguageEquals(string language)
	{
		return new DirectSpecification<MessageTemplate>(t => t.Language == language);
	}

	public static ISpecification<MessageTemplate> LanguageEqualsOrDefault(string language)
	{
		return new DirectSpecification<MessageTemplate>(t => t.Language == language || t.Default);
	}

	public static ISpecification<MessageTemplate> TypeEquals(MessageTemplateType type)
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

	public static ISpecification<MessageTemplate> Matches(string code, MessageTemplateType type, string language)
	{
		ISpecification<MessageTemplate>[] speficications =
		[
			CodeEquals(code),
			LanguageEqualsOrDefault(language),
			TypeEquals(type)
		];

		return new CompositeSpecification<MessageTemplate>(PredicateOperator.AndAlso, speficications);
	}

	public static ISpecification<MessageTemplate> ExistsDefault(string code, MessageTemplateType type, string excludeId)
	{
		ISpecification<MessageTemplate>[] speficications =
		[
			CodeEquals(code),
			TypeEquals(type),
			IsDefault(true),
			IdNotEquals(excludeId)
		];

		return new CompositeSpecification<MessageTemplate>(PredicateOperator.AndAlso, speficications);
	}
}
