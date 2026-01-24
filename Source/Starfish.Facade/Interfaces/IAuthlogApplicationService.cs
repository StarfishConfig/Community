using Nerosoft.Euonia.Application;
using Nerosoft.Starfish.Transit;

namespace Nerosoft.Starfish.Facade.Interfaces;

/// <summary>
/// Defines the application service interface for managing authentication logs.
/// </summary>
public interface IAuthlogApplicationService : IApplicationService
{
	/// <summary>
	/// Searches for authentication logs based on the specified criteria with pagination support.
	/// </summary>
	/// <param name="criteria">The search criteria to filter authentication logs.</param>
	/// <param name="skip">The number of records to skip for pagination.</param>
	/// <param name="size">The maximum number of records to return.</param>
	/// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the list of authentication logs matching the criteria.</returns>
	Task<List<AuthlogListDto>> SearchAsync(AuthlogCriteriaDto criteria, int skip, int size, CancellationToken cancellationToken = default);

	/// <summary>
	/// Counts the total number of authentication logs that match the specified criteria.
	/// </summary>
	/// <param name="criteria">The search criteria to filter authentication logs.</param>
	/// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the total count of authentication logs matching the criteria.</returns>
	Task<int> CountAsync(AuthlogCriteriaDto criteria, CancellationToken cancellationToken = default);
}