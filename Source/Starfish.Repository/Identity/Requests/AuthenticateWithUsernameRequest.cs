using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Repository.Models;

namespace Nerosoft.Starfish.Repository.Requests;

/// <summary>
/// Represents a request to authenticate a user using a username and password.
/// Implements <see cref="IRequest{UserAuthQueryModel}"/> and expects a <see cref="UserAuthQueryModel"/> result on success.
/// </summary>
/// <param name="Username">The username of the user attempting to authenticate.</param>
/// <param name="Password">The plaintext password supplied for authentication.</param>
public record AuthenticateWithUsernameRequest(string Username, string Password) : IRequest<UserAuthQueryModel>;