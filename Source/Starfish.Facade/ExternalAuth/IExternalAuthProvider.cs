namespace Nerosoft.Starfish.Facade.ExternalAuth;

/// <summary>
/// Defines a contract for external authentication providers that exchange an authorization code
/// for an <see cref="ExternalAuthResult"/> representing the authenticated external identity.
/// </summary>
public interface IExternalAuthProvider
{
	/// <summary>
	/// Authenticates with an external provider using the provided authorization code.
	/// </summary>
	/// <param name="authCode">The authorization code or token returned by the external provider's OAuth/OpenID flow.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>
	/// A <see cref="Task{TResult}"/> that represents the asynchronous operation.
	/// The task result contains an <see cref="ExternalAuthResult"/> with information about the authenticated external user.
	/// </returns>
	Task<ExternalAuthResult> AuthenticateAsync(string authCode, CancellationToken cancellationToken = default);
}