using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Repository.Models;

namespace Nerosoft.Starfish.Repository.Requests;

/// <summary>
/// Represents a query request to search for authentication logs with pagination support.
/// </summary>
/// <param name="Criteria">The search criteria to filter authentication logs.</param>
/// <param name="Skip">The number of records to skip for pagination.</param>
/// <param name="Size">The maximum number of records to return.</param>
public record AuthlogSearchQuery(AuthlogCriteriaModel Criteria, int Skip, int Size) : IRequest<IList<AuthlogListModel>>;

/// <summary>
/// Represents a query request to count the total number of authentication logs matching the specified criteria.
/// </summary>
/// <param name="Criteria">The search criteria to filter authentication logs.</param>
public record AuthlogCountQuery(AuthlogCriteriaModel Criteria) : IRequest<int>;