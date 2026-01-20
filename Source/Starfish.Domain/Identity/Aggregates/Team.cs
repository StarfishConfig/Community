using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Nerosoft.Euonia.Business;
using Nerosoft.Starfish.Domain.Repositories;
using Nerosoft.Starfish.Persistent;

namespace Nerosoft.Starfish.Domain.Aggregates;

/// <summary>
/// Represents a team within the system.
/// </summary>
internal sealed class Team : EditableObjectBase<Team, long>
{
	private readonly ObservableCollection<string> _members = new();

	public Team()
	{
		_members.CollectionChanged += OnMembersChanged;
	}

	public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name);
	public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(p => p.Description);
	public static readonly PropertyInfo<string> OwnerIdProperty = RegisterProperty<string>(p => p.OwnerId);
	public static readonly PropertyInfo<ObservableCollection<string>> MembersProperty = RegisterProperty<ObservableCollection<string>>(p => p.Members, nameof(Members), []);

	/// <summary>
	/// Gets the name of the team.
	/// </summary>
	public string Name
	{
		get => GetProperty(NameProperty);
		private set => SetProperty(NameProperty, value);
	}

	/// <summary>
	/// Gets the description of the team.
	/// </summary>
	public string Description
	{
		get => GetProperty(DescriptionProperty);
		private set => SetProperty(DescriptionProperty, value);
	}

	/// <summary>
	/// Gets the identifier of the team owner.
	/// </summary>
	public string OwnerId
	{
		get => GetProperty(OwnerIdProperty);
		private set => SetProperty(OwnerIdProperty, value);
	}

	public IReadOnlyCollection<string> Members
	{
		get => _members;
	}

	protected override void AddRules()
	{
		Rules.AddRule(new CommonRule.Lambda(NameProperty, (_, _) => Task.FromResult(!string.IsNullOrWhiteSpace(Name)), "Team name cannot be empty."));
		Rules.AddRule(new CommonRule.Lambda(DescriptionProperty, (_, _) => Task.FromResult(Description?.Length > 500), "Team description cannot exceed 500 characters."));
		Rules.AddRule(new CommonRule.Lambda(OwnerIdProperty, (_, _) => Task.FromResult(!string.IsNullOrWhiteSpace(OwnerId)), "Owner id cannot be empty."));
	}

	#region Business Methods

	/// <summary>
	/// Sets the name of the team.
	/// </summary>
	/// <param name="name"></param>
	internal void SetName(string name)
	{
		Name = name;
	}

	/// <summary>
	/// Sets the description of the team.
	/// </summary>
	/// <param name="description"></param>
	internal void SetDescription(string description)
	{
		Description = description;
	}

	/// <summary>
	/// Appends a member to the team.
	/// </summary>
	/// <param name="userIds"></param>
	internal void AppendMember(params string[] userIds)
	{
		foreach (var userId in userIds)
		{
			if (_members.Contains(userId))
			{
				continue;
			}

			_members.Add(userId);
		}
	}

	/// <summary>
	/// Removes a member from the team.
	/// </summary>
	/// <param name="userIds">The user identifier.</param>
	internal void RemoveMember(params string[] userIds)
	{
		foreach (var userId in userIds)
		{
			_members.Remove(userId);
		}
	}

	/// <summary>
	/// Transfers ownership of the team to another user.
	/// </summary>
	/// <param name="ownerId"></param>
	/// <param name="leaveAfterTransfer"></param>
	internal void Transfer(string ownerId, bool leaveAfterTransfer)
	{
		OwnerId = ownerId;
		if (!Members.Contains(ownerId))
		{
			AppendMember(ownerId);
		}

		if (leaveAfterTransfer)
		{
			RemoveMember(BusinessContext.User.UserId);
		}
	}

	private void OnMembersChanged(object sender, NotifyCollectionChangedEventArgs args)
	{
		if (!IsBypassingRuleChecks)
		{
			OnPropertyChanged(nameof(Members));
		}
	}

	protected override void Dispose(bool disposing)
	{
		_members.CollectionChanged -= OnMembersChanged;
		base.Dispose(disposing);
	}

	#endregion

	#region Factory Methods

	[FactoryCreate]
	private async Task CreateAsync(string name, CancellationToken cancellationToken = default)
	{
		Name = name;
		OwnerId = BusinessContext.User.UserId;
		AppendMember(BusinessContext.User.UserId);
		await Task.CompletedTask;
	}

	[FactoryFetch]
	private async Task FetchAsync(long id, CancellationToken cancellationToken = default)
	{
		var repository = BusinessContext.GetRequiredService<ITeamRepository>();
		var data = await repository.GetAsync(id, cancellationToken);
		LoadProperty(NameProperty, data.Name);
		LoadProperty(DescriptionProperty, data.Description);
		LoadProperty(OwnerIdProperty, data.OwnerId);
		LoadProperty(IdProperty, data.Id);
		using (BypassRuleChecks)
		{
			foreach (var memberId in data.Members)
			{
				_members.Add(memberId);
			}
		}
	}

	[FactoryInsert]
	protected override Task InsertAsync(CancellationToken cancellationToken = default)
	{
		var data = new TeamData
		{
			Name = Name,
			Description = Description,
			OwnerId = OwnerId,
			Members = Members.ToHashSet()
		};

		var repository = BusinessContext.GetRequiredService<ITeamRepository>();
		return repository.SaveAsync(data, cancellationToken);
	}

	[FactoryUpdate]
	protected override Task UpdateAsync(CancellationToken cancellationToken = default)
	{
		var data = new TeamData(Id)
		{
			Name = Name,
			Description = Description,
			OwnerId = OwnerId,
			Members = Members.ToHashSet()
		};

		var repository = BusinessContext.GetRequiredService<ITeamRepository>();
		return repository.SaveAsync(data, cancellationToken);
	}

	[FactoryDelete]
	protected override Task DeleteAsync(CancellationToken cancellationToken = default)
	{
		var repository = BusinessContext.GetRequiredService<ITeamRepository>();
		return repository.DeleteAsync(Id, cancellationToken);
	}

	#endregion
}