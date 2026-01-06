using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Configuration;

namespace Nerosoft.Starfish.Facade.Auth;

internal abstract class BaseAuthProvider(IConfiguration configuration) : IExternalAuthProvider
{
	protected IConfiguration Configuration { get; } = configuration;

	public abstract Task<ExternalAuthResult> AuthenticateAsync(string authCode, CancellationToken cancellationToken = default);

	protected virtual void ReadJsonValue(JsonObject json, string key, Action<string> action)
	{
		if (!json.TryGetPropertyValue(key, out var node) || node == null)
		{
			return;
		}

		var valueType = node.GetValueKind();
		switch (valueType)
		{
			case JsonValueKind.Number:
				action(node.GetValue<long>().ToString());
				break;
			case JsonValueKind.String:
				action(node.GetValue<string>());
				break;
			case JsonValueKind.True:
			case JsonValueKind.False:
				action(node.GetValue<bool>().ToString());
				break;
		}
	}
}