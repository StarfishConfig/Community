using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Repository.Models;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Requests;

/// <summary>
/// Query for retrieving detailed information about a single team.
/// </summary>
/// <param name="Id">The unique identifier of the team to retrieve.</param>
public record TeamDetailQuery(long Id) : IRequest<TeamDetailModel>;

/// <summary>
/// Query for retrieving base information about a team, with an option to include its members.
/// </summary>
/// <param name="Id">The unique identifier of the team to retrieve.</param>
/// <param name="IncludeMembers">Indicates whether to include the team's members in the response.</param>
public record TeamBaseInfoQuery(long Id, bool IncludeMembers) : IRequest<TeamBaseInfoModel>;

/// <summary>
/// Query for searching teams with an optional role filter and pagination.
/// </summary>
/// <param name="Keyword">Search term to match against team properties (name, description, etc.).</param>
/// <param name="Role">Role filter indicating the current user's relation to the team.</param>
/// <param name="Skip">Number of items to skip for paging.</param>
/// <param name="Size">Maximum number of items to return.</param>
public record TeamSearchQuery(string Keyword, TeamMemberRole Role, int Skip, int Size) : IRequest<IList<TeamListModel>>;

/// <summary>
/// Query to count teams that match the given search keyword and role filter.
/// </summary>
/// <param name="Keyword">Search term to match against team properties.</param>
/// <param name="Role">Role filter indicating the current user's relation to the team.</param>
public record TeamCountQuery(string Keyword, TeamMemberRole Role) : IRequest<int>;

/// <summary>
/// Query for retrieving a paged list of members for a specific team, optionally filtered by keyword.
/// </summary>
/// <param name="TeamId">Identifier of the team whose members are requested.</param>
/// <param name="Keyword">Optional filter applied to member properties (name, email, etc.).</param>
/// <param name="Skip">Number of items to skip for paging.</param>
/// <param name="Size">Maximum number of items to return.</param>
public record TeamMemberListQuery(long TeamId, string Keyword, int Skip, int Size) : IRequest<IList<TeamMemberModel>>;

/// <summary>
/// Query to count members of a specific team that match an optional keyword filter.
/// </summary>
/// <param name="TeamId">Identifier of the team whose members are to be counted.</param>
/// <param name="Keyword">Optional filter applied to member properties.</param>
public record TeamMemberCountQuery(long TeamId, string Keyword) : IRequest<int>;

/// <summary>
/// Query to check whether a given user is the owner of a specified team.
/// </summary>
/// <param name="TeamId">Identifier of the team to check ownership for.</param>
/// <param name="UserId">Identifier of the user to verify as owner.</param>
public record TeamOwnerCheckQuery(long TeamId, string UserId) : IRequest<bool>;

/// <summary>
/// Query to retrieve identifiers of teams where a user has a specific role.
/// </summary>
/// <param name="UserId">Identifier of the user whose team scope is requested.</param>
/// <param name="Role">The role to filter teams by for the given user.</param>
public record TeamScopeQuery(string UserId, TeamMemberRole Role) : IRequest<IList<long>>;

/// <summary>
/// Query to retrieve base information for multiple teams by their IDs.
/// </summary>
/// <param name="Ids"></param>
public record TeamLookupQuery(List<long> Ids) : IRequest<IDictionary<long, string>>;