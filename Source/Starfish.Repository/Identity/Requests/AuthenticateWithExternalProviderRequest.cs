using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Repository.Models;

namespace Nerosoft.Starfish.Repository.Requests;

/// <summary>
/// Request to authenticate a user using an external authentication provider.
/// Implements <see cref="IRequest{UserAuthQueryModel}"/> and, on success,
/// returns a <see cref="UserAuthQueryModel"/> describing the authenticated user.
/// </summary>
/// <param name="Provider">Identifier of the external provider (for example, "google", "github").</param>
/// <param name="OpenId">Provider-specific user identifier (OpenID) obtained from the external provider.</param>
public record AuthenticateWithExternalProviderRequest(string Provider, string OpenId) : IRequest<UserAuthQueryModel>;