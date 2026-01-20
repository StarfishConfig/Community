using Nerosoft.Euonia.Application;
using Nerosoft.Starfish.Facade.Transit;

namespace Nerosoft.Starfish.Facade.Interfaces;

/// <summary>
/// Application service contract for team-related operations.
/// Provides search, retrieval, creation, update and membership management for teams.
/// </summary>
public interface ITeamApplicationService : IApplicationService
{
	/// <summary>
	/// Searches teams by keyword and optional type with pagination.
	/// </summary>
	/// <param name="keyword">Search term to match against team properties (name, description, etc.).</param>
	/// <param name="type">Optional team type filter. Null to ignore type filtering.</param>
	/// <param name="skip">Number of items to skip for pagination.</param>
	/// <param name="size">Maximum number of items to return.</param>
	/// <param name="cancellationToken">Token to cancel the operation.</param>
	/// <returns>A list of <see cref="TeamListDto"/> matching the search criteria.</returns>
	Task<List<TeamListDto>> SearchAsync(string keyword, int? type, int skip, int size, CancellationToken cancellationToken = default);

	/// <summary>
	/// Counts teams that match the given keyword and optional type filter.
	/// </summary>
	/// <param name="keyword">Search term to match against team properties.</param>
	/// <param name="type">Optional team type filter. Null to ignore type filtering.</param>
	/// <param name="cancellationToken">Token to cancel the operation.</param>
	/// <returns>The total number of teams matching the criteria.</returns>
	Task<int> CountAsync(string keyword, int? type, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves detailed information for a specific team by its identifier.
	/// </summary>
	/// <param name="id">Unique identifier of the team.</param>
	/// <param name="cancellationToken">Token to cancel the operation.</param>
	/// <returns>A <see cref="TeamDetailDto"/> containing the team's details.</returns>
	Task<TeamDetailDto> GetAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves members of a team, optionally filtered by a keyword, with pagination.
	/// </summary>
	/// <param name="teamId">Identifier of the team whose members are requested.</param>
	/// <param name="keyword">Optional filter applied to member properties (name, email, etc.).</param>
	/// <param name="skip">Number of items to skip for pagination.</param>
	/// <param name="size">Maximum number of items to return.</param>
	/// <param name="cancellationToken">Token to cancel the operation.</param>
	/// <returns>A list of <see cref="TeamMemberDto"/> for the specified team.</returns>
	Task<List<TeamMemberDto>> GetMemberListAsync(long teamId, string keyword, int skip, int size, CancellationToken cancellationToken = default);

	/// <summary>
	/// Counts the number of members in a team that match the given keyword.
	/// </summary>
	/// <param name="teamId">Identifier of the team whose members are requested.</param>
	/// <param name="keyword">Optional filter applied to member properties (name, email, etc.).</param>
	/// <param name="cancellationToken"></param>
	/// <returns>A number of members for the specified team.</returns>
	Task<int> GetMemberCountAsync(long teamId, string keyword, CancellationToken cancellationToken = default);

	/// <summary>
	/// Creates a new team using the provided data.
	/// </summary>
	/// <param name="data">Team data used to create the new team.</param>
	/// <param name="cancellationToken">Token to cancel the operation.</param>
	Task CreateAsync(TeamEditDto data, CancellationToken cancellationToken = default);

	/// <summary>
	/// Updates an existing team identified by <paramref name="id"/> with the provided data.
	/// </summary>
	/// <param name="id">Identifier of the team to update.</param>
	/// <param name="data">Updated team data.</param>
	/// <param name="cancellationToken">Token to cancel the operation.</param>
	Task UpdateAsync(long id, TeamEditDto data, CancellationToken cancellationToken = default);

	/// <summary>
	/// Appends one or more users to the specified team.
	/// </summary>
	/// <param name="teamId">Identifier of the team to which users will be added.</param>
	/// <param name="userIds">List of user identifiers to append to the team.</param>
	/// <param name="cancellationToken">Token to cancel the operation.</param>
	Task AppendMemberAsync(long teamId, IList<string> userIds, CancellationToken cancellationToken = default);

	/// <summary>
	/// Removes one or more users from the specified team.
	/// </summary>
	/// <param name="teamId">Identifier of the team from which users will be removed.</param>
	/// <param name="userIds">List of user identifiers to remove from the team.</param>
	/// <param name="cancellationToken">Token to cancel the operation.</param>
	Task RemoveMemberAsync(long teamId, IList<string> userIds, CancellationToken cancellationToken = default);
}