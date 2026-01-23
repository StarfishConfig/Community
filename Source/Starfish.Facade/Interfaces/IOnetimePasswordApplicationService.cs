using Nerosoft.Euonia.Application;
using Nerosoft.Starfish.Transit;

namespace Nerosoft.Starfish.Facade.Interfaces;

/// <summary>
///	Defines a contract for requesting onetime passwords (OTP) within the application service layer.
/// </summary>
/// <remarks>Implementations of this interface should generate secure OTPs for verification purposes and ensure
/// that the request process adheres to security best practices. This service is typically used in scenarios where user
/// identity verification is required, such as multi-factor authentication or password reset workflows.</remarks>
public interface IOnetimePasswordApplicationService : IApplicationService
{
	/// <summary>
	/// Request a new onetime password
	/// </summary>
	/// <param name="data"></param>
	/// <param name="cancellationToken"></param>
	/// <returns>The request id of the new OTP to use for verify.</returns>
	Task<string> RequestAsync(OnetimePasswordRequestDto data, CancellationToken cancellationToken = default);
}
