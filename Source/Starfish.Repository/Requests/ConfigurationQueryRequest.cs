using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Repository.Models;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Requests;

/// <summary>
/// Represents a query request to retrieve basic information about a configuration.
/// </summary>
/// <param name="TeamScope">The list of team identifiers that define the scope of teams the requester has access to.</param>
/// <param name="Id">The unique identifier of the configuration to query.</param>
/// <remarks>
/// This query returns a <see cref="ConfigurationBaseInfoModel"/> containing the basic configuration details.
/// The team scope is used to enforce authorization and ensure the requester can only access configurations within their permitted teams.
/// </remarks>
public record ConfigurationBaseInfoQuery(IList<long> TeamScope, long Id) : IRequest<ConfigurationBaseInfoModel>;

/// <summary>
/// Represents a query request to search and retrieve a paginated list of configurations.
/// </summary>
/// <param name="TeamScope">The list of team identifiers that define the scope of teams the requester has access to.</param>
/// <param name="Keyword">The search keyword to filter configurations by name or other searchable fields.</param>
/// <param name="TeamId">The unique identifier of the team to filter configurations by. Use 0 or a default value to search across all teams in scope.</param>
/// <param name="Skip">The number of records to skip for pagination purposes.</param>
/// <param name="Size">The maximum number of records to return in the result set.</param>
/// <remarks>
/// This query returns a list of <see cref="ConfigurationListModel"/> containing the matching configurations.
/// The team scope is used to enforce authorization and ensure the requester can only access configurations within their permitted teams.
/// Results are paginated using the Skip and Size parameters to support efficient data retrieval.
/// </remarks>
public record ConfigurationSearchQuery(IList<long> TeamScope, string Keyword, long TeamId, int Skip, int Size) : IRequest<IList<ConfigurationListModel>>;

/// <summary>
/// Represents a query request to retrieve the total count of configurations matching the specified search criteria.
/// </summary>
/// <param name="TeamScope">The list of team identifiers that define the scope of teams the requester has access to.</param>
/// <param name="Keyword">The search keyword to filter configurations by name or other searchable fields.</param>
/// <param name="TeamId">The unique identifier of the team to filter configurations by. Use 0 or a default value to count across all teams in scope.</param>
/// <remarks>
/// This query returns an integer representing the total number of configurations that match the specified criteria.
/// The team scope is used to enforce authorization and ensure the requester can only count configurations within their permitted teams.
/// This query is typically used in conjunction with <see cref="ConfigurationSearchQuery"/> to support pagination scenarios.
/// </remarks>
public record ConfigurationCountQuery(IList<long> TeamScope, string Keyword, long TeamId) : IRequest<int>;

public record ConfigurationDetailQuery(IList<long> TeamScope, long Id) : IRequest<ConfigurationDetailModel>;

public record ConfigurationPermissionGrantQuery(IList<long> TeamScope, string UserId) : IRequest<IReadOnlyDictionary<long, ConfigurationPermissionGrantState>>;