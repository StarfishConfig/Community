using Nerosoft.Starfish.Persistent.Data;

namespace Nerosoft.Starfish.Persistent.Repositories;

/// <summary>
/// Repository interface for managing one-time passwords.
/// </summary>
internal interface IOnetimePasswordRepository : IRepository
{
	/// <summary>
	/// Asynchronously saves the specified one-time password data to the underlying data store.
	/// </summary>
	/// <remarks>This method may throw exceptions if the save operation fails due to invalid data or other
	/// issues.</remarks>
	/// <param name="data">The one-time password data to be saved. This parameter cannot be null.</param>
	/// <param name="cancellationToken">A cancellation token that can be used to cancel the operation. The default value is <see
	/// cref="CancellationToken.None"/>.</param>
	/// <returns>A task that represents the asynchronous save operation.</returns>
	Task SaveAsync(OnetimePasswordData data, CancellationToken cancellationToken = default);

	/// <summary>
	/// Asynchronously retrieves the one-time password data associated with the specified request identifier.
	/// </summary>
	/// <remarks>If the specified request identifier does not correspond to any stored one-time password data, the
	/// method returns <see langword="null"/>. Callers should handle potential exceptions that may occur during the
	/// operation, such as those related to data access or cancellation.</remarks>
	/// <param name="requestId">The unique identifier for the request associated with the one-time password. This parameter cannot be null or
	/// empty.</param>
	/// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation. The default value is <see
	/// cref="CancellationToken.None"/>.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the one-time password data associated
	/// with the specified request identifier, or <see langword="null"/> if no matching data is found.</returns>
	Task<OnetimePasswordData> GetAsync(string requestId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Asynchronously updates the checked status of a request identified by the specified request ID at the given
	/// timestamp.
	/// </summary>
	/// <remarks>If the operation is canceled via the provided cancellation token, the returned task will be
	/// canceled. Ensure that the request ID corresponds to an existing request before calling this method.</remarks>
	/// <param name="requestId">The unique identifier of the request to update. Cannot be null or empty.</param>
	/// <param name="checkedAt">The date and time when the request was checked.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	Task CheckOffAsync(string requestId, DateTime checkedAt, CancellationToken cancellationToken = default);
}