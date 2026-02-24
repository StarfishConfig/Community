using System.Text.RegularExpressions;
using Nerosoft.Euonia.Osba;
using Nerosoft.Starfish.Domain.Commands;
using Nerosoft.Starfish.Domain.Events;
using Nerosoft.Starfish.Persistent.Data;
using Nerosoft.Starfish.Persistent.Repositories;
using Nerosoft.Starfish.Shared;
using Nerosoft.Starfish.Toolkit;

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
	public static readonly PropertyInfo<string> SecretProperty = RegisterProperty<string>(p => p.Secret);
	public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(p => p.Description);
	public static readonly PropertyInfo<ConfigurationStatus> StatusProperty = RegisterProperty<ConfigurationStatus>(p => p.Status);
	public static readonly PropertyInfo<ObservableList<ConfigurationPermission>> PermissionsProperty = RegisterProperty<ObservableList<ConfigurationPermission>>(p => p.Permissions);
	public static readonly PropertyInfo<ObservableList<ConfigurationItem>> ItemsProperty = RegisterProperty<ObservableList<ConfigurationItem>>(p => p.Items);

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
	/// Gets the secret associated with the configuration.
	/// </summary>
	public string Secret
	{
		get => GetProperty(SecretProperty);
		private set => SetProperty(SecretProperty, value);
	}

	/// <summary>
	/// Gets or sets the description of the configuration.
	/// </summary>
	public string Description
	{
		get => GetProperty(DescriptionProperty);
		private set => SetProperty(DescriptionProperty, value);
	}

	/// <summary>
	/// Gets the status of the configuration.
	/// </summary>
	public ConfigurationStatus Status
	{
		get => GetProperty(StatusProperty);
		private set => SetProperty(StatusProperty, value);
	}

	/// <summary>
	/// Gets the collection of configuration permissions associated with the current instance.
	/// </summary>
	public ObservableList<ConfigurationPermission> Permissions
	{
		get => GetProperty(PermissionsProperty);
		private set => SetProperty(PermissionsProperty, value);
	}

	/// <summary>
	/// Gets the collection of configuration items associated with this instance.
	/// </summary>
	/// <remarks>The returned collection is observable and will notify listeners of changes. The property is
	/// read-only; items can be added or removed from the collection, but the collection reference itself cannot be
	/// replaced externally.</remarks>
	public ObservableList<ConfigurationItem> Items
	{
		get => GetProperty(ItemsProperty);
		private set => SetProperty(ItemsProperty, value);
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
					context.AddErrorResult($"The configuration code '{value}' already exists within the team.");
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
	/// Encrypts and stores the specified secret value.
	/// </summary>
	/// <param name="secret">The plain text secret to encrypt and store. Cannot be null.</param>
	public void SetSecret(string secret)
	{
		Secret = Cryptography.AES.Encrypt(secret);
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

	/// <summary>
	/// Sets the permission grant states for multiple users based on the specified mapping.
	/// </summary>
	/// <remarks>If a user's grant state is set to None, their permissions are removed. Granting higher-level
	/// permissions (such as Publish) will also grant all lower-level permissions (Write and Read) for that user.</remarks>
	/// <param name="permissions">A dictionary mapping user identifiers to their desired permission grant states. Each entry specifies the
	/// permissions to assign or remove for the corresponding user.</param>
	public void SetPermissions(Dictionary<string, ConfigurationPermissionGrantState> permissions)
	{
		var factory = BusinessContext.GetRequiredService<IObjectFactory>();

		foreach (var (userId, state) in permissions)
		{
			var permission = Permissions.FirstOrDefault(p => p.UserId == userId);
			if (permission == null && state != ConfigurationPermissionGrantState.None)
			{
				permission = factory.Create<ConfigurationPermission>(userId);
			}

			if (state == ConfigurationPermissionGrantState.None)
			{
				Permissions.Remove(permission);
			}
			else
			{
				if (state.HasFlag(ConfigurationPermissionGrantState.Publish))
				{
					permission.AllowPublish = true;
					permission.AllowWrite = true;
					permission.AllowRead = true;
				}
				if (state.HasFlag(ConfigurationPermissionGrantState.Write))
				{
					permission.AllowWrite = true;
					permission.AllowRead = true;
				}
				if (state.HasFlag(ConfigurationPermissionGrantState.Read))
				{
					permission.AllowRead = true;
				}
			}
		}
	}

	/// <summary>
	/// Removes the permission entry associated with the specified user identifier, if it exists.
	/// </summary>
	/// <remarks>If no permission entry exists for the specified user identifier, the method performs no
	/// action.</remarks>
	/// <param name="userId">The unique identifier of the user whose permission should be removed. Cannot be null.</param>
	public void RemovePermission(string userId)
	{
		var permission = Permissions.FirstOrDefault(p => p.UserId == userId);
		if (permission != null)
		{
			Permissions.Remove(permission);
		}
	}

	#endregion

	#region Factory Methods
	[FactoryCreate]
	protected override Task CreateAsync(CancellationToken cancellationToken = default)
	{
		Status = ConfigurationStatus.Pending;
		LoadProperty(PermissionsProperty, []);
		return base.CreateAsync(cancellationToken);
	}

	[FactoryFetch]
	protected override Task FetchAsync(long id, CancellationToken cancellationToken = default)
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