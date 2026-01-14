using System.Security.Claims;
using Duende.IdentityModel;
using Microsoft.Extensions.Configuration;
using Nerosoft.Euonia.Application;
using Nerosoft.Euonia.Bus;
using Nerosoft.Euonia.Domain;
using Nerosoft.Starfish.Facade.Events;
using Nerosoft.Starfish.Facade.ExternalAuth;
using Nerosoft.Starfish.Facade.Interfaces;
using Nerosoft.Starfish.Facade.Transit;
using Nerosoft.Starfish.Repository.Models;
using Nerosoft.Starfish.Repository.Requests;
using Nerosoft.Starfish.Shared;
using Nerosoft.Starfish.Toolkit;

namespace Nerosoft.Starfish.Facade.Implements;

/// <summary>
/// Provides authentication services for handling user authentication through various providers.
/// </summary>
internal class AuthApplicationService(IConfiguration configuration) : BaseApplicationService, IAuthApplicationService
{
	private const string JWT_AUTH_SECTION = "Authentication:Bearer";

	/// <summary>
	/// Authenticates a user and grants access by creating a claims principal.
	/// </summary>
	/// <param name="data">The authentication request data containing provider information and credentials.</param>
	/// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
	/// <returns>A <see cref="ClaimsPrincipal"/> containing the authenticated user's claims including subject, name, email, phone, nickname, and roles.</returns>
	/// <remarks>
	/// This method performs the following operations:
	/// <list type="number">
	/// <item><description>Validates and processes the authentication request based on the provider type.</description></item>
	/// <item><description>Publishes a <see cref="UserAuthSuccessEvent"/> upon successful authentication.</description></item>
	/// <item><description>Publishes a <see cref="TokenRefreshedEvent"/> if the provider is a refresh token.</description></item>
	/// <item><description>Creates a claims identity with user information including JWT standard claims and roles.</description></item>
	/// <item><description>Publishes a <see cref="UserAuthFailureEvent"/> if authentication fails.</description></item>
	/// </list>
	/// </remarks>
	/// <exception cref="ArgumentException">Thrown when the authentication provider is not specified.</exception>
	/// <exception cref="NotSupportedException">Thrown when the specified authentication provider is not supported.</exception>
	/// <exception cref="BadGatewayException">Thrown when external authentication fails.</exception>
	public async Task<TokenGrantResponseDto> GrantAsync(TokenGrantRequestDto data, CancellationToken cancellationToken = default)
	{
		var events = new List<ApplicationEvent>();

		try
		{
			var request = await GetRequestAsync(data, cancellationToken);
			var user = await Bus.CallAsync(request, cancellationToken);

			var issueAt = DateTime.UtcNow;

			@events.Add(new UserAuthSuccessEvent
			{
				Source = "Bearer",
				GrantType = data.GrantType,
				UserId = user.Id,
				Username = user.Username,
				GrantTime = issueAt //DateTimeHelper.GetDateTimeFromUnixTime(result.IssueAt)
			});

			var refreshTokenId = ObjectId.NewGuid(GuidType.SequentialAsString).ToString("N");
			
			var identity = BuildClaims("Bearer", user);
			events.Add(new TokenGeneratedEvent
			{
				UserId = user.Id,
				Username = user.Username,
				RefreshToken = refreshTokenId,
				GrantTime = issueAt
			});
			if (string.Equals(data.GrantType, AuthProvider.RefreshToken, StringComparison.OrdinalIgnoreCase))
			{
				@events.Add(new TokenRefreshedEvent
				{
					OriginToken = data.Password
				});
			}

			//var roles = user.Roles?.Select(r => r.Name);

			//var jti = ObjectId.NewGuid(GuidType.SequentialAsString).ToString("N");

			var issueTime = DateTime.UtcNow;
			var expiresAt = issueTime.AddDays(1);

			var builder = TokenGenerator.From(identity)
			                            .WithSigningKey(configuration.GetValue<string>($"{JWT_AUTH_SECTION}:SigningKey"))
			                            .WithIssuer(configuration.GetValue<string>($"{JWT_AUTH_SECTION}:Issuer:0"))
			                            .IssuedAt(issueTime);

			var accessToken = builder.Build();

			return new TokenGrantResponseDto
			{
				AccessToken = accessToken,
				RefreshToken = ObjectId.NewGuid(GuidType.SequentialAsString).ToString("N"),
				TokenType = TokenType.Bearer,
				Username = user.Username,
				UserId = user.Id,
				IssueAt = new DateTimeOffset(issueTime).ToUnixTimeSeconds(),
				ExpiresIn = (long)(expiresAt - issueTime).TotalSeconds
			};
		}
		catch (Exception exception)
		{
			events.Add(new UserAuthFailureEvent
			{
				Source = "Bearer",
				GrantType = data.GrantType,
				GrantTime = DateTime.UtcNow,
				Data = new Dictionary<string, string>
				{
					{ "Username", data.Username ?? string.Empty },
					{ "Password", data.Password != null ? "******" : string.Empty },
				},
				Error = exception.Message
			});
			throw;
		}
		finally
		{
			if (events.Count > 0)
			{
				await Parallel.ForEachAsync(events, cancellationToken, async (@event, token) => await Bus.PublishAsync(@event, token));
			}
		}
	}

	public async Task<ClaimsPrincipal> SignInAsync(TokenGrantRequestDto data, CancellationToken cancellationToken = default)
	{
		var events = new List<ApplicationEvent>();
		try
		{
			var request = await GetRequestAsync(data, cancellationToken);
			var user = await Bus.CallAsync(request, cancellationToken);

			var issueAt = DateTime.UtcNow;

			@events.Add(new UserAuthSuccessEvent
			{
				Source = "Cookie",
				GrantType = data.GrantType,
				UserId = user.Id,
				Username = user.Username,
				GrantTime = issueAt //DateTimeHelper.GetDateTimeFromUnixTime(result.IssueAt)
			});

			var identity = BuildClaims("Cookie", user);
			return new ClaimsPrincipal(identity);
		}
		catch (Exception exception)
		{
			events.Add(new UserAuthFailureEvent
			{
				Source = "Cookie",
				GrantType = data.GrantType,
				GrantTime = DateTime.UtcNow,
				Data = new Dictionary<string, string>
				{
					{ "Username", data.Username ?? string.Empty },
					{ "Password", data.Password != null ? "******" : string.Empty },
				},
				Error = exception.Message
			});
			throw;
		}
		finally
		{
			if (events.Count > 0)
			{
				await Parallel.ForEachAsync(events, cancellationToken, async (@event, token) => await Bus.PublishAsync(@event, token));
			}
		}
	}

	private static ClaimsIdentity BuildClaims(string authenticationType, UserAuthQueryModel user)
	{
		var identity = new ClaimsIdentity(authenticationType);

		switch (authenticationType)
		{
			case "Jwt":
			case "Bearer":
				identity.AddClaim(new Claim(JwtClaimTypes.Subject, user.Id));
				identity.AddClaim(new Claim(JwtClaimTypes.Name, user.Username));
				identity.AddClaim(new Claim(JwtClaimTypes.Email, user.Email ?? string.Empty));
				identity.AddClaim(new Claim(JwtClaimTypes.PhoneNumber, user.Phone));
				identity.AddClaim(new Claim(JwtClaimTypes.NickName, user.Nickname ?? string.Empty));
				break;
			case "Cookie":
				identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
				identity.AddClaim(new Claim(ClaimTypes.Name, user.Username));
				identity.AddClaim(new Claim(ClaimTypes.Email, user.Email ?? string.Empty));
				identity.AddClaim(new Claim(ClaimTypes.MobilePhone, user.Phone));
				identity.AddClaim(new Claim("nickname", user.Nickname ?? string.Empty));
				break;
		}

		foreach (var role in user.Roles)
		{
			identity.AddClaim(new Claim(ClaimTypes.Role, role));
		}

		return identity;
	}

	/// <summary>
	/// Creates an authentication request based on the specified provider type.
	/// </summary>
	/// <param name="data">The authentication request data containing provider information and credentials.</param>
	/// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
	/// <returns>An <see cref="IRequest{UserAuthQueryModel}"/> instance appropriate for the specified authentication provider.</returns>
	/// <remarks>
	/// Supported authentication providers:
	/// <list type="bullet">
	/// <item><description><see cref="AuthProvider.Username"/> and <see cref="AuthProvider.Password"/>: Username/password authentication.</description></item>
	/// <item><description><see cref="AuthProvider.RefreshToken"/>: Refresh token authentication.</description></item>
	/// <item><description><see cref="AuthProvider.Microsoft"/>: Microsoft external authentication.</description></item>
	/// <item><description><see cref="AuthProvider.Google"/>: Google external authentication.</description></item>
	/// <item><description><see cref="AuthProvider.Github"/>: GitHub external authentication.</description></item>
	/// <item><description><see cref="AuthProvider.Facebook"/>: Facebook external authentication.</description></item>
	/// </list>
	/// </remarks>
	/// <exception cref="ArgumentException">Thrown when the provider is null or empty.</exception>
	/// <exception cref="NotSupportedException">Thrown when the specified authentication provider is not supported or the external provider service is not available.</exception>
	/// <exception cref="BadGatewayException">Thrown when external authentication with a third-party provider fails.</exception>
	private async Task<IRequest<UserAuthQueryModel>> GetRequestAsync(TokenGrantRequestDto data, CancellationToken cancellationToken = default)
	{
		switch (data.GrantType?.ToLowerInvariant())
		{
			case null or "":
				throw new ArgumentException(IdentityResources.IDS_ERROR_AUTH_PROVIDER_REQUIRED, nameof(data));
			case AuthProvider.Username:
			case AuthProvider.Password:
				return new AuthenticateWithUsernameRequest(data.Username, data.Password);
			case AuthProvider.RefreshToken:
				return new AuthenticateWithRefreshTokenRequest(data.Password);
			case AuthProvider.Microsoft:
			case AuthProvider.Google:
			case AuthProvider.Github:
			case AuthProvider.Facebook:
			{
				var provider = LazyServiceProvider.GetKeyedService<IExternalAuthProvider>(data.GrantType.ToLowerInvariant());
				if (provider == null)
				{
					throw new NotSupportedException();
				}

				var auth = await provider.AuthenticateAsync(data.Username, cancellationToken);

				if (auth == null)
				{
					throw new BadGatewayException(IdentityResources.IDS_ERROR_EXTERNAL_AUTH_FAILED);
				}

				{
				}

				return new AuthenticateWithExternalProviderRequest(data.GrantType, auth.Id);
			}
			default:
				throw new NotSupportedException(string.Format(IdentityResources.IDS_ERROR_AUTH_PROVIDER_NOT_SUPPORT, data.GrantType));
		}
	}
}