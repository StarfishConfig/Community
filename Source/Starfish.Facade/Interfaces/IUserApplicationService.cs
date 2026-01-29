using Nerosoft.Euonia.Application;
using Nerosoft.Starfish.Transit;

namespace Nerosoft.Starfish.Facade.Interfaces;

/// <summary>
/// Defines application-level user operations exposed by the facade layer.
/// Implementations handle user CRUD, lookup, profile management and password operations.
/// </summary>
public interface IUserApplicationService : IApplicationService
{
    /// <summary>
    /// Retrieves a user's detailed information by identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A <see cref="UserDetailDto"/> containing detailed user information.</returns>
    Task<UserDetailDto> GetAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches users by a keyword and optional locked state, returning a paged list.
    /// </summary>
    /// <param name="keywork">Search keyword to match against user fields (name, email, etc.).</param>
    /// <param name="locked">Optional filter to include only locked/unlocked users. Null means no filter.</param>
    /// <param name="skip">Number of items to skip for paging.</param>
    /// <param name="size">Maximum number of items to return.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>List of <see cref="UserListDto"/> matching the search criteria.</returns>
    Task<List<UserListDto>> SearchAsync(string keywork, bool? locked, int skip, int size, CancellationToken cancellationToken = default);

    /// <summary>
    /// Counts users that match the provided keyword and optional locked state.
    /// </summary>
    /// <param name="keywork">Search keyword to match against user fields.</param>
    /// <param name="locked">Optional locked filter. Null means count all regardless of locked state.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The total number of users matching the criteria.</returns>
    Task<int> CountAsync(string keywork, bool? locked, CancellationToken cancellationToken = default);

    /// <summary>
    /// Looks up users matching a keyword and returns a small set suitable for autocomplete or selection lists.
    /// </summary>
    /// <param name="keyword">Search keyword for lookup.</param>
    /// <param name="size">Maximum number of lookup results to return.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>List of <see cref="UserLookupDto"/> for matching users.</returns>
    Task<List<UserLookupDto>> LookupAsync(string keyword, int size, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the profile of the currently authenticated user.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>The current user's <see cref="UserProfileDto"/>.</returns>
    Task<UserProfileDto> GetProfileAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new user from the provided data.
    /// </summary>
    /// <param name="data">Data required to create the user.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous create operation.</returns>
    Task CreateAsync(UserCreateDto data, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing user's data.
    /// </summary>
    /// <param name="id">Identifier of the user to update.</param>
    /// <param name="data">Updated user data.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous update operation.</returns>
    Task UpdateAsync(string id, UserUpdateDto data, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the profile of the currently authenticated user.
    /// </summary>
    /// <param name="data">Profile fields to update.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous profile update operation.</returns>
    Task UpdateProfileAsync(UserUpdateDto data, CancellationToken cancellationToken = default);

    /// <summary>
    /// Changes the password for a user based on the provided change request (typically requires current password).
    /// </summary>
    /// <param name="data">Password change request containing current and new password information.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous password change operation.</returns>
    Task ChangePasswordAsync(UserPasswordChangeDto data, CancellationToken cancellationToken = default);

    /// <summary>
    /// Resets a user's password using provided reset data (e.g., token and new password).
    /// </summary>
    /// <param name="data">Password reset information such as reset token and new password.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous password reset operation.</returns>
    Task ResetPasswordAsync(UserPasswordResetDto data, CancellationToken cancellationToken = default);

    /// <summary>
    /// Resets the password for the user identified by <paramref name="id"/>, returning the new password or a generated token.
    /// </summary>
    /// <param name="id">Identifier of the user whose password should be reset.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A string result, typically the new password or a reset token.</returns>
    Task<string> ResetPasswordAsync(string id, CancellationToken cancellationToken = default);
}