using Nerosoft.Euonia.Osba;
using Nerosoft.Starfish.Domain.Rules;

namespace Nerosoft.Starfish.Domain.Aggregates;

internal class Project : EditableObjectBase<Project, long>
{
	#region Properties
	public static readonly PropertyInfo<long> TeamIdProperty = RegisterProperty<long>(p => p.TeamId);

	/// <summary>
	/// Gets or sets the team ID associated with the project.
	/// </summary>
	public long TeamId
	{
		get => GetProperty(TeamIdProperty);
		private set => SetProperty(TeamIdProperty, value);
	}

	public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name);

	/// <summary>
	/// Gets or sets the name for the project.
	/// </summary>
	/// <remarks>
	/// This property is required and must not be null or whitespace.
	/// </remarks>
	public string Name
	{
		get => GetProperty(NameProperty);
		private set => SetProperty(NameProperty, value);
	}

	public static readonly PropertyInfo<string> ImageProperty = RegisterProperty<string>(p => p.Image);

	/// <summary>
	/// Gets the image URL for the project.
	/// </summary>
	public string Image
	{
		get => GetProperty(ImageProperty);
		private set => SetProperty(ImageProperty, value);
	}

	public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(p => p.Description);

	/// <summary>
	/// Gets or sets the description for the project.
	/// </summary>
	public string Description
	{
		get => GetProperty(DescriptionProperty);
		private set => SetProperty(DescriptionProperty, value);
	}
	#endregion

	#region Rules & Business Methods
	protected override void AddRules()
	{
		Rules.AddRule<Project>(TeamIdProperty, target => Task.FromResult(target.TeamId > 0), ProjectResources.IDS_ERROR_TEAM_ID_REQUIRED);
		Rules.AddRule(new ProjectNameCheckRule(NameProperty));
	}
	#endregion

	#region Factory Methods

	[FactoryCreate]
	protected override Task CreateAsync(CancellationToken cancellationToken = default)
	{
		return Task.CompletedTask;
	}

	[FactoryFetch]
	protected override async Task FetchAsync(long id, CancellationToken cancellationToken = default)
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
