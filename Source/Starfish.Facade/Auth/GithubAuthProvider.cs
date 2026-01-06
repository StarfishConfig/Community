using System.Text.Json.Nodes;
using FluentHttpClient;
using Microsoft.Extensions.Configuration;

namespace Nerosoft.Starfish.Facade.Auth;

internal class GithubAuthProvider(IConfiguration configuration) : BaseAuthProvider(configuration)
{
	public override async Task<ExternalAuthResult> AuthenticateAsync(string authCode, CancellationToken cancellationToken = default)
	{
		var token = await GetTokenAsync(authCode, cancellationToken);
		var user = await GetUserAsync(token, cancellationToken);

		var result = new ExternalAuthResult();

		ReadJsonValue(user, "id", id => result.Id = id);
		ReadJsonValue(user, "login", login => result.Username = login);
		ReadJsonValue(user, "name", name => result.Nickname = name);
		ReadJsonValue(user, "email", email => result.Email = email);
		ReadJsonValue(user, "avatar_url", avatarUrl => result.AvatarUrl = avatarUrl);

		return result;
	}

	private async ValueTask<JsonObject> GetUserAsync(string token, CancellationToken cancellationToken = default)
	{
		using var client = new HttpClient();
		client.BaseAddress = new Uri("https://api.github.com");
		return await client.UsingRoute("/user")
		                   .WithHeader("User-Agent", "Linkyou")
		                   .WithHeader("Accept", "application/json")
		                   .WithOAuthBearerToken(token)
		                   .GetAsync(cancellationToken)
		                   .ReadJsonObjectAsync(cancellationToken);
	}

	private async Task<string> GetTokenAsync(string code, CancellationToken cancellationToken = default)
	{
		var secret = Configuration.GetValue<string>("OAuth:Github:ClientSecret");
		var clientId = Configuration.GetValue<string>("OAuth:Github:ClientId");

		using var client = new HttpClient();
		client.BaseAddress = new Uri("https://github.com");
		var response = await client.UsingRoute("/login/oauth/access_token")
		                           .WithHeader("Accept", "application/json")
		                           .WithQueryParameter("client_id", clientId)
		                           .WithQueryParameter("client_secret", secret)
		                           .WithQueryParameter("code", code)
		                           .PostAsync(cancellationToken)
		                           .ReadJsonAsync<Dictionary<string, string>>(cancellationToken);

		if (response == null)
		{
			throw new Exception("Could not get access token");
		}

		if (!response.TryGetValue("access_token", out var token))
		{
			throw new BadGatewayException();
		}

		{
		}

		return token;
	}
}