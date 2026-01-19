using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Repository.Models;

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

/// <summary>
/// Represents a request to query detailed information for a specific user.
/// </summary>
/// <param name="Id">The identifier of the user whose details are being requested.</param>
/// <remarks>
/// The returned <see cref="UserDetailModel"/> may contain sensitive user information.
/// Handlers should enforce authorization checks and avoid logging or exposing sensitive fields.
/// </remarks>
public record UserDetailQuery(string Id) : IRequest<UserDetailModel>;

public record UserSearchQuery(string Keyword, bool? Locked, int Skip = 0, int Size = 20) : IRequest<List<UserListModel>>;

public record UserCountQuery(string Keyword, bool? Locked) : IRequest<int>;