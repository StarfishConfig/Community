using System.Text.Json.Nodes;
using FluentHttpClient;
using Microsoft.Extensions.Configuration;

namespace Nerosoft.Starfish.Facade.ExternalAuth;

internal class GoogleAuthProvider(IConfiguration configuration) : BaseAuthProvider(configuration)
{
	public override async Task<ExternalAuthResult> AuthenticateAsync(string authCode, CancellationToken cancellationToken = default)
	{
		var token = await GetTokenAsync(authCode, cancellationToken);
		var user = await GetUserAsync(token, cancellationToken);

		var result = new ExternalAuthResult();

		ReadJsonValue(user, "sub", id => result.Id = id);
		ReadJsonValue(user, "email", email => result.Username = email);
		ReadJsonValue(user, "name", name => result.Nickname = name);
		ReadJsonValue(user, "picture", avatarUrl => result.AvatarUrl = avatarUrl);

		return result;
	}

	private async Task<JsonObject> GetUserAsync(string token, CancellationToken cancellationToken = default)
	{
		using var client = new HttpClient();
		return await client.UsingRoute("https://www.googleapis.com/oauth2/v3/userinfo")
		                   .WithHeader("User-Agent", "Linkyou")
		                   .WithHeader("Accept", "application/json")
		                   .WithOAuthBearerToken(token)
		                   .GetAsync(cancellationToken)
		                   .ReadJsonObjectAsync(cancellationToken);
	}

	private async Task<string> GetTokenAsync(string code, CancellationToken cancellationToken = default)
	{
		var secret = Configuration.GetValue<string>("OAuth:Google:ClientSecret");
		var clientId = Configuration.GetValue<string>("OAuth:Google:ClientId");
		var redirectUri = Configuration.GetValue<string>("OAuth:RedirectUri");

		using var client = new HttpClient();
		client.BaseAddress = new Uri("https://oauth2.googleapis.com");

		var formContent = new Dictionary<string, string>
		{
			{ "client_id", clientId },
			{ "client_secret", secret },
			{ "code", code },
			{ "redirect_uri", redirectUri },
			{ "grant_type", "authorization_code" }
		};

		var response = await client.UsingRoute("/token")
		                           .WithHeader("Accept", "application/json")
		                           .WithContent(new FormUrlEncodedContent(formContent))
		                           .PostAsync(cancellationToken)
		                           .ReadJsonObjectAsync(cancellationToken);
		if (response == null)
		{
			throw new BadGatewayException("Failed to get token from Google.");
		}

		if (response.TryGetPropertyValue("access_token", out var token) == false || token == null)
		{
			throw new BadGatewayException("Failed to get access token from Google.");
		}

		return token.GetValue<string>();
	}
}