using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Repository.Models;

namespace Nerosoft.Starfish.Repository.Requests;

/// <summary>
/// Represents a request to query detailed information for a specific user.
/// </summary>
/// <param name="Id">The identifier of the user whose details are being requested.</param>
/// <remarks>
/// The returned <see cref="UserDetailQueryModel"/> may contain sensitive user information.
/// Handlers should enforce authorization checks and avoid logging or exposing sensitive fields.
/// </remarks>
public record UserDetailQuery(string Id) : IRequest<UserDetailQueryModel>;