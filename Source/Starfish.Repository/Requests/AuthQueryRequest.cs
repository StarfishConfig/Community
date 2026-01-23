using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Repository.Models;

namespace Nerosoft.Starfish.Repository.Requests;

/// <summary>
/// Represents a request to authenticate a user using a username and password.
/// Implements <see cref="IRequest{TResponse}"/> and expects a <see cref="UserAuthInfoModel"/> result on success.
/// </summary>
/// <param name="Username">The username of the user attempting to authenticate.</param>
/// <param name="Password">The plaintext password supplied for authentication.</param>
public record AuthWithUsernameRequest(string Username, string Password) : IRequest<UserAuthInfoModel>;

/// <summary>
/// Request to authenticate a user using a refresh token.
/// Implements <see cref="IRequest{UserAuthQueryModel}"/> and,
/// on success, returns a <see cref="UserAuthInfoModel"/> describing the authenticated user.
/// </summary>
/// <param name="RefreshToken">A valid refresh token previously issued to the client. The token is exchanged for a new authentication result.</param>
public record AuthWithRefreshTokenRequest(string RefreshToken) : IRequest<UserAuthInfoModel>;

/// <summary>
/// Request to authenticate a user using an external authentication provider.
/// Implements <see cref="IRequest{UserAuthQueryModel}"/> and, on success,
/// returns a <see cref="UserAuthInfoModel"/> describing the authenticated user.
/// </summary>
/// <param name="Provider">Identifier of the external provider (for example, "google", "github").</param>
/// <param name="OpenId">Provider-specific user identifier (OpenID) obtained from the external provider.</param>
public record AuthWithExternalProviderRequest(string Provider, string OpenId) : IRequest<UserAuthInfoModel>;