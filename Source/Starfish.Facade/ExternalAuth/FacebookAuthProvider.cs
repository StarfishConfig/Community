using System.Text.Json.Nodes;
using FluentHttpClient;
using Microsoft.Extensions.Configuration;

namespace Nerosoft.Starfish.Facade.ExternalAuth;

internal class FacebookAuthProvider(IConfiguration configuration) : BaseAuthProvider(configuration)
{
	public override async Task<ExternalAuthResult> AuthenticateAsync(string authCode, CancellationToken cancellationToken = default)
	{
		var token = await GetTokenAsync(authCode, cancellationToken);
		var user = await GetUserAsync(token, cancellationToken);
		var result = new ExternalAuthResult();
		ReadJsonValue(user, "id", id => result.Id = id);
		ReadJsonValue(user, "name", name => result.Nickname = name);
		ReadJsonValue(user, "email", email => result.Email = email);
		//ReadJsonValue(user, "picture", picture => result.AvatarUrl = picture?.GetValue<JsonObject>()?["data"]?.GetValue<string>("url"));
		return result;
	}

	private async ValueTask<JsonObject> GetUserAsync(string token, CancellationToken cancellationToken = default)
	{
		using var client = new HttpClient();
		return await client.UsingRoute("https://graph.facebook.com/v12.0/me?fields=id,name")
		                   .WithHeader("User-Agent", "Linkyou")
		                   .WithHeader("Accept", "application/json")
		                   .WithOAuthBearerToken(token)
		                   .WithQueryParameter("fields", "id,name,email,picture")
		                   .GetAsync(cancellationToken)
		                   .ReadJsonObjectAsync(cancellationToken);
	}

	private async Task<string> GetTokenAsync(string code, CancellationToken cancellationToken = default)
	{
		var secret = Configuration.GetValue<string>("OAuth:Microsoft:ClientSecret");
		var clientId = Configuration.GetValue<string>("OAuth:Microsoft:ClientId");
		var redirectUri = Configuration.GetValue<string>("OAuth:RedirectUri");

		using var client = new HttpClient();
		client.BaseAddress = new Uri("https://graph.facebook.com");

		var response = await client.UsingRoute("/v22.0/oauth/access_token")
		                           .WithHeader("Accept", "application/json")
		                           .WithQueryParameter("client_id", clientId)
		                           .WithQueryParameter("client_secret", secret)
		                           .WithQueryParameter("code", code)
		                           .WithQueryParameter("redirect_uri", redirectUri)
		                           .GetAsync(cancellationToken)
		                           .ReadJsonObjectAsync(cancellationToken);

		if (response == null)
		{
			throw new BadGatewayException("Failed to get token from Facebook.");
		}

		if (response.TryGetPropertyValue("access_token", out var token) == false || token == null)
		{
			throw new BadGatewayException("Failed to get access token from Facebook.");
		}

		return token.GetValue<string>();
	}
}