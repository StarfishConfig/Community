using Nerosoft.Euonia.Application;
using Nerosoft.Starfish.Facade.Dtos;

namespace Nerosoft.Starfish.Facade.Interfaces;

/// <summary>
/// Defines the contract for authentication-related application services.
/// </summary>
public interface IAuthApplicationService : IApplicationService
{
	/// <summary>
	/// Grants the access token based on the specified authentication request data.
	/// </summary>
	/// <param name="data">The authentication request data transfer object containing the required credentials.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the authentication response data transfer object.</returns>
	Task<AuthResponseDto> GrantAsync(AuthRequestDto data, CancellationToken cancellationToken = default);
}