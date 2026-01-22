using System.Security.Claims;
using Nerosoft.Euonia.Application;
using Nerosoft.Starfish.Transit;

namespace Nerosoft.Starfish.Facade.Interfaces;

/// <summary>
/// Defines the contract for authentication-related application services.
/// </summary>
public interface IAuthApplicationService : IApplicationService
{
	/// <summary>
	/// Authenticates a user and grants access by returning an authentication response.
	/// </summary>
	/// <param name="data">The authentication request data transfer object containing the required credentials.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the authentication response data transfer object.</returns>
	Task<TokenGrantResponseDto> GrantAsync(TokenGrantRequestDto data, CancellationToken cancellationToken = default);

	/// <summary>
	/// Signs in a user by creating a claims principal based on the provided authentication data, returning the principal for further processing.
	/// </summary>
	/// <param name="data">The authentication request data transfer object containing the required credentials.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	Task<ClaimsPrincipal> SignInAsync(TokenGrantRequestDto data, CancellationToken cancellationToken = default);
}