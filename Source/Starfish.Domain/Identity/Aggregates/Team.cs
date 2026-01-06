using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Nerosoft.Euonia.Business;

namespace Nerosoft.Starfish.Domain.Aggregates;

/// <summary>
/// Represents a team within the system.
/// </summary>
internal sealed partial class Team : EditableObjectBase<Team, string>
{
	public Team()
	{
		Members.CollectionChanged += OnMembersChanged;
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

	public ObservableCollection<string> Members => GetProperty(MembersProperty);

	protected override void AddRules()
	{
		Rules.AddRule(new CommonRule.Lambda(NameProperty, async (_, _) => !string.IsNullOrWhiteSpace(Name), "Team name cannot be empty."));
		Rules.AddRule(new CommonRule.Lambda(DescriptionProperty, async (_, _) => Description?.Length > 500, "Team description cannot exceed 500 characters."));
	}

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
	/// <param name="userId"></param>
	internal void AppendMember(string userId)
	{
		if (!Members.Contains(userId))
		{
			Members.Add(userId);
		}

		OnPropertyChanged(nameof(Members));
	}

	/// <summary>
	/// Removes a member from the team.
	/// </summary>
	/// <param name="userId">The user identifier.</param>
	internal void RemoveMember(string userId)
	{
		if (Members.Contains(userId))
		{
			Members.Remove(userId);
		}

		OnPropertyChanged(nameof(Members));
	}

	private void OnMembersChanged(object sender, NotifyCollectionChangedEventArgs args)
	{
		if (!IsBypassingRuleChecks)
		{
		}
	}

	protected override void Dispose(bool disposing)
	{
		Members.CollectionChanged -= OnMembersChanged;
		base.Dispose(disposing);
	}
}