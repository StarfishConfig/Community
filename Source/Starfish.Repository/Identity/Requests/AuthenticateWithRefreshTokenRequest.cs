using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Repository.Models;

namespace Nerosoft.Starfish.Repository.Requests;

/// <summary>
/// Request to authenticate a user using a refresh token.
/// Implements <see cref="IRequest{UserAuthQueryModel}"/> and,
/// on success, returns a <see cref="UserAuthQueryModel"/> describing the authenticated user.
/// </summary>
/// <param name="RefreshToken">A valid refresh token previously issued to the client. The token is exchanged for a new authentication result.</param>
public record AuthenticateWithRefreshTokenRequest(string RefreshToken) : IRequest<UserAuthQueryModel>;