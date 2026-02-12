using System.Text.RegularExpressions;
using Nerosoft.Euonia.Osba;
using Nerosoft.Starfish.Domain.Commands;
using Nerosoft.Starfish.Domain.Events;
using Nerosoft.Starfish.Persistent.Data;
using Nerosoft.Starfish.Persistent.Repositories;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Domain.Aggregates;

/// <summary>
/// Represents a configuration entity.
/// </summary>
internal class Configuration : EditableObjectBase<Configuration, long>
{
	#region Properties

	public static readonly PropertyInfo<long> TeamIdProperty = RegisterProperty<long>(p => p.TeamId);
	public static readonly PropertyInfo<string> CodeProperty = RegisterProperty<string>(p => p.Code);
	public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name);
	public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(p => p.Description);

	/// <summary>
	/// Gets or sets the team identifier associated with the configuration.
	/// </summary>
	/// <remarks>
	///	- Must be greater than zero.
	/// - Must correspond to an existing team in the system.
	/// </remarks>
	public long TeamId
	{
		get => GetProperty(TeamIdProperty);
		private set => SetProperty(TeamIdProperty, value);
	}

	/// <summary>
	/// Gets or sets the vanity identifier of the configuration.
	/// </summary>
	/// <remarks>
	///	The code must be unique within the team.
	/// </remarks>
	public string Code
	{
		get => GetProperty(CodeProperty);
		private set => SetProperty(CodeProperty, value);
	}

	/// <summary>
	/// Gets or sets the name of the configuration.
	/// </summary>
	public string Name
	{
		get => GetProperty(NameProperty);
		private set => SetProperty(NameProperty, value);
	}

	/// <summary>
	/// Gets or sets the description of the configuration.
	/// </summary>
	public string Description
	{
		get => GetProperty(DescriptionProperty);
		private set => SetProperty(DescriptionProperty, value);
	}

	#endregion

	#region Rules

	protected override void AddRules()
	{
		Rules.AddRule<Configuration>(TeamIdProperty, _ => Task.FromResult(TeamId > 0), "TeamId must be greater than zero.");
		Rules.AddRule(new CodeCheckRule(CodeProperty));
		Rules.AddRule<Configuration>(DescriptionProperty, _ => Task.FromResult(string.IsNullOrWhiteSpace(Description) || Description.Length <= 1000), "Description cannot exceed 1000 characters.");
	}

	private class CodeCheckRule(IPropertyInfo property) : RuleBase(property)
	{
		public override async Task ExecuteAsync(IRuleContext context, CancellationToken cancellationToken = default)
		{
			// Ensure the target object is of type UserGeneralBusiness.
			if (context.Target is not IEditableObject target)
			{
				return;
			}

			if (target.IsDeleted)
			{
				return;
			}

			var value = target.ReadProperty(Property)?.ToString();

			if (string.IsNullOrWhiteSpace(value))
			{
				context.AddErrorResult("Code is required.");
			}
			else if (Regex.IsMatch(value, RegexPattern.ConfigurationCode))
			{
				context.AddErrorResult("Code contains invalid characters.");
			}
			else
			{
				var repository = target.BusinessContext.GetRequiredService<IConfigurationRepository>();
				var teamId = target.ReadProperty(TeamIdProperty);

				var excludeId = target.IsNew ? 0 : target.ReadProperty(IdProperty);

				var exists = await repository.CheckCodeExistsAsync(teamId, value, excludeId, cancellationToken);

				if (exists)
				{
					context.AddErrorResult($"A configuration with the code '{value}' already exists in the team.");
				}
			}
		}
	}

	#endregion

	#region Business Methods

	/// <summary>
	/// Sets the code of the configuration.
	/// </summary>
	/// <param name="code"></param>
	public void SetCode(string code)
	{
		if (string.Equals(Code, code, StringComparison.InvariantCultureIgnoreCase))
		{
			return;
		}

		Code = code;
	}

	/// <summary>
	/// Sets the name of the configuration.
	/// </summary>
	/// <param name="name"></param>
	public void SetName(string name)
	{
		if (string.Equals(Name, name))
		{
			return;
		}

		Name = name;
	}

	/// <summary>
	/// Sets the description of the configuration.
	/// </summary>
	/// <param name="description"></param>
	public void SetDescription(string description)
	{
		if (string.Equals(Description, description))
		{
			return;
		}

		Description = description;
	}
	#endregion

	#region Factory Methods

	[FactoryCreate]
	private async Task CreateAsync(ConfigurationCreateCommand command, CancellationToken cancellationToken = default)
	{
		TeamId = command.TeamId;
		Code = command.Code;
		Name = command.Name;
		Description = command.Description;
		await Task.CompletedTask;
	}

	[FactoryFetch]
	private Task FetchAsync(long id, CancellationToken cancellationToken = default)
	{
		var repository = BusinessContext.GetRequiredService<IConfigurationRepository>();
		var factory = BusinessContext.GetRequiredService<IObjectFactory>();
		return repository.GetAsync(id, cancellationToken)
						 .ContinueWith(async task =>
						 {
							 task.WaitAndUnwrapException(cancellationToken);
							 var result = task.Result;
							 if (result == null)
							 {
								 throw new NotFoundException();
							 }

							 LoadProperty(IdProperty, result.Id);
							 LoadProperty(TeamIdProperty, result.TeamId);
							 LoadProperty(CodeProperty, result.Code);
							 LoadProperty(NameProperty, result.Name);
							 LoadProperty(DescriptionProperty, result.Description);
						 }, cancellationToken);
	}

	[FactoryInsert]
	protected override async Task InsertAsync(CancellationToken cancellationToken = default)
	{
		var data = new ConfigurationData
		{
			TeamId = TeamId,
			Code = Code,
			Name = Name,
			Description = Description
		};
		var repository = BusinessContext.GetRequiredService<IConfigurationRepository>();
		var id = await repository.SaveAsync(data, cancellationToken);
		RaiseEvent(new ConfigurationCreatedEvent { Id = id, TeamId = TeamId, Code = Code, Name = Name });
	}

	[FactoryUpdate]
	protected override Task UpdateAsync(CancellationToken cancellationToken = default)
	{
		var data = new ConfigurationData(Id)
		{
			TeamId = TeamId,
			Code = Code,
			Name = Name,
			Description = Description
		};

		var repository = BusinessContext.GetRequiredService<IConfigurationRepository>();
		return repository.SaveAsync(data, cancellationToken)
						 .ContinueWith(task =>
						 {
							 task.WaitAndUnwrapException(cancellationToken);
							 //RaiseEvent(new ConfigurationUpdatedEvent { Id = Id, TeamId = TeamId, Code = Code, Name = Name });
						 }, cancellationToken);
	}

	[FactoryDelete]
	protected override Task DeleteAsync(CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	#endregion
}