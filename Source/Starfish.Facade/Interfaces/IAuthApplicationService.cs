using System.Security.Claims;
using Nerosoft.Euonia.Application;
using Nerosoft.Starfish.Facade.Transit;

namespace Nerosoft.Starfish.Facade.Interfaces;

/// <summary>
/// Defines the contract for authentication-related application services.
/// </summary>
public interface IAuthApplicationService : IApplicationService
{
	/// <summary>
	/// Authenticates a user and grants access by creating a claims principal.
	/// </summary>
	/// <param name="authenticationType">The authentication scheme type used for creating the claims' identity.</param>
	/// <param name="data">The authentication request data transfer object containing the required credentials.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the authentication response data transfer object.</returns>
	Task<ClaimsPrincipal> GrantAsync(string authenticationType, AuthRequestDto data, CancellationToken cancellationToken = default);
}