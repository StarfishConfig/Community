using Nerosoft.Starfish.Persistent;

namespace Nerosoft.Starfish.Domain.Repositories;

/// <summary>
/// Defines the contract for user repository operations.
/// </summary>
internal interface IUserRepository
{
	/// <summary>
	/// Retrieves user data by username.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task SaveAsync(UserData data, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves user data by identifier.
	/// </summary>
	/// <param name="id"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<UserData> GetAsync(string id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Checks if a phone number already exists in the repository.
	/// </summary>
	/// <param name="phone"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<bool> ExistsPhoneAsync(string phone, CancellationToken cancellationToken = default);

	/// <summary>
	/// Checks if an email already exists in the repository.
	/// </summary>
	/// <param name="email"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<bool> ExistsEmailAsync(string email, CancellationToken cancellationToken = default);

	/// <summary>
	/// Checks if a username already exists in the repository.
	/// </summary>
	/// <param name="username"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	Task<bool> ExistsUsernameAsync(string username, CancellationToken cancellationToken = default);
}