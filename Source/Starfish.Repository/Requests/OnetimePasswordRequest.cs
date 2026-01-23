using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Repository.Models;

namespace Nerosoft.Starfish.Repository.Requests;

/// <summary>
/// Represents a request to retrieve the details of a one-time password associated with a specific request identifier.
/// </summary>
/// <param name="RequestId">The unique identifier for the request used to fetch the corresponding one-time password details. Cannot be null or
/// empty.</param>
public record OnetimePasswordDetailQuery(string RequestId) : IRequest<OnetimePasswordDetailModel>;
