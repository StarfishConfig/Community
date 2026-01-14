using Nerosoft.Euonia.Bus;

namespace Nerosoft.Starfish.Repository.Requests;

/// <summary>
/// Represents a request to verify a user's password.
/// The request is handled by an authentication component which returns true when the provided password
/// matches the stored credential for the specified user.
/// </summary>
/// <param name="Id">The identifier of the user whose password will be verified.</param>
/// <param name="Password">
/// The plaintext password to verify. This value is sensitive and must not be logged or persisted in plaintext.
/// Handlers should perform secure hashing and constant-time comparison when validating this value.
/// </param>
/// <remarks>
/// Implementations should ensure secure handling of credentials, enforce rate limiting, and avoid exposing
/// password values in error messages or logs.
/// </remarks>
public record UserPasswordVerifyRequest(string Id, string Password) : IRequest<bool>;