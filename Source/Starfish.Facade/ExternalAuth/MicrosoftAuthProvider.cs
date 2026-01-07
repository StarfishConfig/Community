using System.Text.Json.Nodes;
using FluentHttpClient;
using Microsoft.Extensions.Configuration;

namespace Nerosoft.Starfish.Facade.ExternalAuth;

internal class MicrosoftAuthProvider(IConfiguration configuration) : BaseAuthProvider(configuration)
{
	public override async Task<ExternalAuthResult> AuthenticateAsync(string authCode, CancellationToken cancellationToken = default)
	{
		var token = await GetTokenAsync(authCode, cancellationToken);
		var user = await GetUserAsync(token, cancellationToken);

		var result = new ExternalAuthResult();

		ReadJsonValue(user, "id", id => result.Id = id);
		ReadJsonValue(user, "userPrincipalName", login => result.Username = login);
		ReadJsonValue(user, "displayName", name => result.Nickname = name);
		ReadJsonValue(user, "email", email => result.Email = email);
		ReadJsonValue(user, "mobilePhone", avatarUrl => result.Phone = avatarUrl);

		return result;
	}

	private async ValueTask<JsonObject> GetUserAsync(string token, CancellationToken cancellationToken = default)
	{
		using var client = new HttpClient();
		return await client.UsingRoute("https://graph.microsoft.com/v1.0/me")
		                   .WithHeader("User-Agent", "Linkyou")
		                   .WithHeader("Accept", "application/json")
		                   .WithOAuthBearerToken(token)
		                   .GetAsync(cancellationToken)
		                   .ReadJsonObjectAsync(cancellationToken);
	}

	private async Task<string> GetTokenAsync(string code, CancellationToken cancellationToken = default)
	{
		var secret = Configuration.GetValue<string>("OAuth:Microsoft:ClientSecret");
		var clientId = Configuration.GetValue<string>("OAuth:Microsoft:ClientId");
		var redirectUri = Configuration.GetValue<string>("OAuth:RedirectUri");

		using var client = new HttpClient();
		client.BaseAddress = new Uri("https://login.microsoftonline.com");

		var formContent = new Dictionary<string, string>
		{
			{ "client_id", clientId },
			{ "client_secret", secret },
			{ "code", code },
			{ "redirect_uri", redirectUri },
			{ "grant_type", "authorization_code" },
			{ "scope", "User.Read Mail.Read" }
		};

		var response = await client.UsingRoute("/consumers/oauth2/v2.0/token")
		                           .WithHeader("Accept", "application/json")
		                           .WithContent(new FormUrlEncodedContent(formContent))
		                           .PostAsync(cancellationToken)
		                           .ReadJsonObjectAsync(cancellationToken);
		if (response == null)
		{
			throw new BadGatewayException("Failed to get token from Microsoft.");
		}

		if (response.TryGetPropertyValue("access_token", out var token) == false || token == null)
		{
			throw new BadGatewayException("Failed to get access token from Microsoft.");
		}

		return token.GetValue<string>();
	}
}