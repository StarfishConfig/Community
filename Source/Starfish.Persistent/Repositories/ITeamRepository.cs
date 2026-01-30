using Nerosoft.Starfish.Persistent;

namespace Nerosoft.Starfish.Domain.Repositories;

/// <summary>
/// Defines repository operations for persisting and retrieving <see cref="TeamData"/> entities.
/// Implementations handle storage concerns (e.g., database, in-memory cache).
/// </summary>
internal interface ITeamRepository : IRepository
{
	/// <summary>
	/// Persists the specified <see cref="TeamData"/> instance.
	/// Creates or updates the team record as appropriate.
	/// </summary>
	/// <param name="data">The team data to save.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A <see cref="Task"/> that completes when the save operation finishes.</returns>
	Task SaveAsync(TeamData data, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves a <see cref="TeamData"/> by its numeric identifier.
	/// </summary>
	/// <param name="id">The unique identifier of the team to retrieve.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>
	/// A <see cref="Task{TResult}"/> whose result is the <see cref="TeamData"/> if found; otherwise <c>null</c>.
	/// </returns>
	Task<TeamData> GetAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes the team identified by the specified id.
	/// If the team does not exist, the implementation may treat this as a no-op or throw, depending on design.
	/// </summary>
	/// <param name="id">The unique identifier of the team to delete.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A <see cref="Task"/> that completes when the delete operation finishes.</returns>
	Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}