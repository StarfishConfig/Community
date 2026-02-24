namespace Nerosoft.Starfish.Persistent.Repositories;

internal interface IProjectRepository : IRepository
{
	Task<bool> CheckNameDuplicationAsync(long teamId, string name, long excludeId = 0, CancellationToken cancellationToken = default);
}
