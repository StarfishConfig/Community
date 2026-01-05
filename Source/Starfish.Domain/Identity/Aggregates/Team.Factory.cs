using Nerosoft.Euonia.Business;

namespace Nerosoft.Starfish.Domain.Aggregates;

internal partial class Team
{
	[FactoryCreate]
	private async Task CreateAsync(string name, CancellationToken cancellationToken = default)
	{
		Name = name;
		await Task.CompletedTask;
	}

	[FactoryFetch]
	private async Task FetchAsync(string id, CancellationToken cancellationToken = default)
	{
		await Task.CompletedTask;
	}
}