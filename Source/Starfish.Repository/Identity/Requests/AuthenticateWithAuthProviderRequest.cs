using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Repository.Models;

namespace Nerosoft.Starfish.Repository.Requests;

/// <summary>
/// Request to authenticate a user via an external authentication provider.
/// Implements <see cref="IRequest{UserAuthQueryModel}"/> and returns a
/// <see cref="AuthInfoModel"/> on successful authentication.
/// </summary>
/// <param name="Provider">Identifier of the external provider (for example, "google", "facebook").</param>
/// <param name="OpenId">Provider-specific OpenID or unique identifier for the user.</param>
public record AuthenticateWithAuthProviderRequest(string Provider, string OpenId) : IRequest<AuthInfoModel>;