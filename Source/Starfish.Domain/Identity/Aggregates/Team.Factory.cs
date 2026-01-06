using Nerosoft.Euonia.Business;

namespace Nerosoft.Starfish.Domain.Aggregates;

internal partial class Team
{
	[FactoryCreate]
	private async Task CreateAsync(string name, string userId, CancellationToken cancellationToken = default)
	{
		Name = name;
		OwnerId = userId;
		AppendMember(userId);
		await Task.CompletedTask;
	}

	[FactoryFetch]
	private async Task FetchAsync(string id, CancellationToken cancellationToken = default)
	{
		await Task.CompletedTask;
	}

	[FactoryInsert]
	protected override Task InsertAsync(CancellationToken cancellationToken = default)
	{
		return base.InsertAsync(cancellationToken);
	}

	[FactoryUpdate]
	protected override Task UpdateAsync(CancellationToken cancellationToken = default)
	{
		return base.UpdateAsync(cancellationToken);
	}

	[FactoryDelete]
	protected override Task DeleteAsync(CancellationToken cancellationToken = default)
	{
		return base.DeleteAsync(cancellationToken);
	}
}