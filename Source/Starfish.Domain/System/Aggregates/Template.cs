using Nerosoft.Euonia.Business;
using Nerosoft.Starfish.Domain.Commands;
using Nerosoft.Starfish.Persistent.Data;
using Nerosoft.Starfish.Persistent.Repositories;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Domain.Aggregates;

internal class Template : EditableObjectBase<Template, long>
{
	#region Properties

	public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name);
	public static readonly PropertyInfo<string> CodeProperty = RegisterProperty<string>(p => p.Code);
	public static readonly PropertyInfo<string> LanguageProperty = RegisterProperty<string>(p => p.Language);
	public static readonly PropertyInfo<TemplateType> TypeProperty = RegisterProperty<TemplateType>(p => p.Type);
	public static readonly PropertyInfo<string> SubjectProperty = RegisterProperty<string>(p => p.Subject);
	public static readonly PropertyInfo<string> BodyProperty = RegisterProperty<string>(p => p.Body);
	public static readonly PropertyInfo<bool> DefaultProperty = RegisterProperty<bool>(p => p.Default);

	public string Name
	{
		get => GetProperty(NameProperty);
		set => SetProperty(NameProperty, value);
	}

	public string Code
	{
		get => GetProperty(CodeProperty);
		set => SetProperty(CodeProperty, value);
	}

	public string Language
	{
		get => GetProperty(LanguageProperty);
		set => SetProperty(LanguageProperty, value);
	}

	public TemplateType Type
	{
		get => GetProperty(TypeProperty);
		set => SetProperty(TypeProperty, value);
	}

	public string Subject
	{
		get => GetProperty(SubjectProperty);
		set => SetProperty(SubjectProperty, value);
	}

	public string Body
	{
		get => GetProperty(BodyProperty);
		set => SetProperty(BodyProperty, value);
	}

	public bool Default
	{
		get => GetProperty(DefaultProperty);
		set => SetProperty(DefaultProperty, value);
	}

	#endregion

	#region Rules

	protected override void AddRules()
	{
		// Name
		Rules.AddRule(new CommonRule.Required(NameProperty, "The name is required."));
		// Code
		Rules.AddRule(new CommonRule.Required(CodeProperty, "The code is required."));
		// Language
		Rules.AddRule(new CommonRule.Required(LanguageProperty, "The language is required."));
		// Type
		Rules.AddRule(new CommonRule.Required(TypeProperty, "The type is required."));
		// Body
		Rules.AddRule(new CommonRule.Required(BodyProperty, "The body is required."));

		Rules.AddRule<Template>(DefaultProperty, async target =>
		{
			if (target.Default == false)
			{
				return true;
			}

			var repository = target.BusinessContext.GetRequiredService<ITemplateRepository>();

			var exists = await repository.ExistsDefaultAsync(target.Code, target.Type, target.Id);

			return !exists;
		}, "Only one default template is allowed for the same usage and type.");
	}

	#endregion

	[FactoryCreate]
	private async Task CreateAsync(TemplateCreateCommand command, CancellationToken cancellationToken = default)
	{
		if (command == null)
		{
			throw new ArgumentNullException(nameof(command));
		}

		Name = command.Name;
		Code = command.Code;
		Type = command.Type;
		Body = command.Body;
		Language = command.Language;
		Subject = command.Subject;
		Default = command.Default;
		await Task.CompletedTask;
	}

	[FactoryFetch]
	private async Task FetchAsync(long id, CancellationToken cancellationToken = default)
	{
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);

		var repository = BusinessContext.GetRequiredService<ITemplateRepository>();

		var data = await repository.GetAsync(id, cancellationToken);

		if (data == null)
		{
			throw new NotFoundException("Message template not found.");
		}

		LoadProperty(IdProperty, data.Id);
		LoadProperty(NameProperty, data.Name);
		LoadProperty(CodeProperty, data.Code);
		LoadProperty(TypeProperty, data.Type);
		LoadProperty(BodyProperty, data.Body);
		LoadProperty(LanguageProperty, data.Language);
		LoadProperty(SubjectProperty, data.Subject);
		LoadProperty(DefaultProperty, data.Default);
	}

	[FactoryInsert]
	protected override Task InsertAsync(CancellationToken cancellationToken = default)
	{
		var data = new TemplateData
		{
			Name = Name,
			Code = Code,
			Body = Body,
			Language = Language,
			Subject = Subject,
			Default = Default,
			Type = Type,
		};

		var repository = BusinessContext.GetRequiredService<ITemplateRepository>();

		return repository.SaveAsync(data, cancellationToken)
		                 .ContinueWith(task =>
		                 {
			                 task.WaitAndUnwrapException(cancellationToken);
			                 LoadProperty(IdProperty, task.Result);
		                 }, cancellationToken);
	}

	[FactoryUpdate]
	protected override Task UpdateAsync(CancellationToken cancellationToken = default)
	{
		var data = new TemplateData(Id)
		{
			Name = Name,
			Code = Code,
			Body = Body,
			Language = Language,
			Subject = Subject,
			Default = Default,
			Type = Type,
		};
		var repository = BusinessContext.GetRequiredService<ITemplateRepository>();
		return repository.SaveAsync(data, cancellationToken);
	}

	[FactoryDelete]
	protected override Task DeleteAsync(CancellationToken cancellationToken = default)
	{
		var repository = BusinessContext.GetRequiredService<ITemplateRepository>();
		return repository.DeleteAsync(Id, cancellationToken);
	}
}