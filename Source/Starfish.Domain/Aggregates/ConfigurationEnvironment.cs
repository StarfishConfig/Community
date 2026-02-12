using System.Text.RegularExpressions;
using Nerosoft.Euonia.Osba;
using Nerosoft.Starfish.Domain.Commands;
using Nerosoft.Starfish.Persistent.Data;
using Nerosoft.Starfish.Shared;
using Nerosoft.Starfish.Toolkit;

namespace Nerosoft.Starfish.Domain.Aggregates;

/// <summary>
/// Represents a configuration environment.
/// </summary>
internal class ConfigurationEnvironment : EditableObjectBase<ConfigurationEnvironment, long>
{
	private const string NAME_PATTERN = "^[a-zA-Z0-9_-]{2,32}$";

	#region Properties
	public static readonly PropertyInfo<long> ConfigurationIdProperty = RegisterProperty<long>(p => p.ConfigurationId);
	public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name);
	public static readonly PropertyInfo<string> SecretProperty = RegisterProperty<string>(p => p.Secret);
	public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(p => p.Description);
	public static readonly PropertyInfo<ConfigurationStatus> StatusProperty = RegisterProperty<ConfigurationStatus>(p => p.Status);
	public static readonly PropertyInfo<ObservableList<ConfigurationPermission>> PermissionsProperty = RegisterProperty<ObservableList<ConfigurationPermission>>(p => p.Permissions);
	public static readonly PropertyInfo<ObservableList<ConfigurationItem>> ItemsProperty = RegisterProperty<ObservableList<ConfigurationItem>>(p => p.Items);
	public long ConfigurationId
	{
		get => GetProperty(ConfigurationIdProperty);
		private set => SetProperty(ConfigurationIdProperty, value);
	}

	/// <summary>
	/// Gets the name of the configuration environment.
	/// </summary>
	public string Name
	{
		get => GetProperty(NameProperty);
		private set => SetProperty(NameProperty, value);
	}

	/// <summary>
	/// Gets the secret associated with the configuration environment.
	/// </summary>
	public string Secret
	{
		get => GetProperty(SecretProperty);
		private set => SetProperty(SecretProperty, value);
	}

	/// <summary>
	/// Gets the description of the configuration environment.
	/// </summary>
	public string Description
	{
		get => GetProperty(DescriptionProperty);
		private set => SetProperty(DescriptionProperty, value);
	}

	/// <summary>
	/// Gets the status of the configuration environment.
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

	#region Business Methods & Rules
	protected override void AddRules()
	{
		Rules.AddRule<ConfigurationEnvironment>(NameProperty, e => Task.FromResult(!string.IsNullOrWhiteSpace(e.Name) && Regex.IsMatch(e.Name, NAME_PATTERN)), "Name cannot be empty.");
		Rules.AddRule<ConfigurationEnvironment>(ConfigurationIdProperty, e => Task.FromResult(e.ConfigurationId > 0), "ConfigurationId must be greater than zero.");
	}

	/// <summary>
	/// Sets the name for the current instance.
	/// </summary>
	/// <param name="name">The new name to assign. Cannot be null.</param>
	public void SetName(string name)
	{
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
	/// Sets the description for the current instance.
	/// </summary>
	/// <param name="description">The new description to assign. Can be null or empty to clear the description.</param>
	public void SetDescription(string description)
	{
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
	private async Task CreateAsync(long configurationId, CancellationToken cancellationToken = default)
	{
		ConfigurationId = configurationId;
		LoadProperty(PermissionsProperty, []);
		await Task.CompletedTask;
	}

	[FactoryFetch]
	private async Task FetchAsync(long id, CancellationToken cancellationToken = default)
	{

	}

	[FactoryInsert]
	protected override async Task InsertAsync(CancellationToken cancellationToken = default)
	{

	}

	[FactoryUpdate]
	protected override async Task UpdateAsync(CancellationToken cancellationToken = default)
	{

	}

	[FactoryDelete]
	protected override async Task DeleteAsync(CancellationToken cancellationToken = default)
	{

	}
	#endregion
}