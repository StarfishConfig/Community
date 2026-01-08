using System.Security.Claims;
using Duende.IdentityModel;
using Microsoft.Extensions.Configuration;
using Nerosoft.Euonia.Application;
using Nerosoft.Euonia.Bus;
using Nerosoft.Euonia.Domain;
using Nerosoft.Starfish.Facade.ExternalAuth;
using Nerosoft.Starfish.Facade.Transit;
using Nerosoft.Starfish.Facade.Events;
using Nerosoft.Starfish.Facade.Interfaces;
using Nerosoft.Starfish.Repository.Models;
using Nerosoft.Starfish.Repository.Requests;
using Nerosoft.Starfish.Shared;
using Nerosoft.Starfish.Toolkit;

namespace Nerosoft.Starfish.Facade.Implements;

internal class AuthApplicationService(IConfiguration configuration) : BaseApplicationService, IAuthApplicationService
{
	private const string JWT_AUTH_SECTION = "JwtAuthenticationOptions";

	public async Task<ClaimsPrincipal> GrantAsync(string authenticationType, AuthRequestDto data, CancellationToken cancellationToken = default)
	{
		var events = new List<ApplicationEvent>();

		try
		{
			var request = await GetRequestAsync(data, cancellationToken);
			var user = await Bus.CallAsync(request, cancellationToken);
			var result = GenerateAccessToken(user);

			@events.Add(new UserAuthSuccessEvent
			{
				AuthType = data.Provider,
				RefreshToken = result.RefreshToken,
				UserId = result.UserId,
				Username = result.Username,
				TokenIssueTime = DateTimeHelper.GetDateTimeFromUnixTime(result.IssueAt)
			});

			if (string.Equals(data.Provider, AuthProvider.RefreshToken, StringComparison.OrdinalIgnoreCase))
			{
				@events.Add(new TokenRefreshedEvent
				{
					OriginToken = data.Password
				});
			}

			var identity = new ClaimsIdentity(authenticationType);
			identity.AddClaim(new Claim(JwtClaimTypes.Subject, result.UserId));
			identity.AddClaim(new Claim(JwtClaimTypes.Name, result.Username));
			identity.AddClaim(new Claim(JwtClaimTypes.Email, user.Email ?? string.Empty));
			identity.AddClaim(new Claim(JwtClaimTypes.PhoneNumber, user.Phone));
			identity.AddClaim(new Claim(JwtClaimTypes.NickName, user.Nickname ?? string.Empty));
			foreach (var role in user.Roles)
			{
				identity.AddClaim(new Claim(ClaimTypes.Role, role));
			}

			return new ClaimsPrincipal(identity);
		}
		catch (Exception exception)
		{
			events.Add(new UserAuthFailureEvent
			{
				AuthType = data.Provider,
				Data = new Dictionary<string, string>
				{
					{ "Username", data.Username ?? string.Empty },
					{ "Password", data.Password != null ? "******" : string.Empty },
				},
				Error = exception.Message,
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

	private async Task<IRequest<UserAuthQueryModel>> GetRequestAsync(AuthRequestDto data, CancellationToken cancellationToken = default)
	{
		switch (data.Provider?.ToLowerInvariant())
		{
			case null or "":
				throw new ArgumentException("Provider must be specified for authentication.", nameof(data));
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
				var provider = LazyServiceProvider.GetKeyedService<IExternalAuthProvider>(data.Provider.ToLowerInvariant());
				if (provider == null)
				{
					throw new NotSupportedException();
				}

				var auth = await provider.AuthenticateAsync(data.Username, cancellationToken);

				if (auth == null)
				{
					throw new BadGatewayException("External authentication failed.");
				}

				{
				}

				return new AuthenticateWithExternalProviderRequest(data.Provider, auth.Id);
			}
			default:
				throw new NotSupportedException($"The authentication provider '{data.Provider}' is not supported.");
		}
	}

	private AuthResponseDto GenerateAccessToken(UserAuthQueryModel user)
	{
		//var roles = user.Roles?.Select(r => r.Name);

		//var jti = ObjectId.NewGuid(GuidType.SequentialAsString).ToString("N");

		var issueTime = DateTime.UtcNow;
		var expiresAt = issueTime.AddDays(1);

		var builder = TokenGenerator.Create(user.Id, user.Username)
		                            .WithSigningKey(configuration.GetValue<string>($"{JWT_AUTH_SECTION}:SigningKey"))
		                            .WithIssuer(configuration.GetValue<string>($"{JWT_AUTH_SECTION}:Issuer:0"))
		                            .AddRole(user.Roles?.ToArray())
		                            .IssuedAt(issueTime)
		                            .AddClaim(JwtClaimTypes.Email, user.Email)
		                            .AddClaim(JwtClaimTypes.PhoneNumber, user.Phone)
		                            .AddClaim(JwtClaimTypes.NickName, user.Nickname);

		var accessToken = builder.Build();

		return new AuthResponseDto
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
}