using Microsoft.Extensions.DependencyInjection;
using Nerosoft.Euonia.Bus;
using Nerosoft.Euonia.Modularity;
using Nerosoft.Euonia.Pipeline;

namespace Nerosoft.Starfish.Facade;

internal class RequestInfoBehavior<TMessage, TResponse> : IPipelineBehavior<TMessage, TResponse>
	where TMessage : class, IRoutedMessage
{
	private readonly IRequestContextAccessor _accessor;

	public RequestInfoBehavior(IServiceProvider provider)
	{
		_accessor = provider.GetService<IRequestContextAccessor>();
	}

	public Task<TResponse> HandleAsync(TMessage context, PipelineDelegate<TMessage, TResponse> next)
	{
		context.Metadata["RequestId"] = _accessor.Context.TraceIdentifier;
		context.Metadata["IpAddress"] = GetRemoteIpAddress();
		context.Metadata["UserAgent"] = GetHeaderValue("User-Agent");
		context.Metadata["Referer"] = GetHeaderValue("Referer");
		context.Metadata["AppName"] = GetHeaderValue("x-client-name");
		context.Metadata["AppVersion"] = GetHeaderValue("x-client-version");
		return next(context);
	}

	private string GetRemoteIpAddress()
	{
		var headers = _accessor?.Context?.RequestHeaders;
		if (headers == null)
		{
			return null;
		}

		var headerKeys = new[]
		{
			"X-Forwarded-For",
			"x-client-ip",
			"CF-Connecting-IP",
			"X-Forwarded-Client-Ip",
			"X-Real-IP",
			"CLIENT-IP"
		};

		foreach (var headerKey in headerKeys)
		{
			if (headers.TryGetValue(headerKey, out var ip) && !string.IsNullOrWhiteSpace(ip))
			{
				return ip;
			}
		}

		{
		}

		return _accessor.Context.RemoteIpAddress?.ToString();
	}

	private string GetHeaderValue(string headerName)
	{
		if (_accessor?.Context?.RequestHeaders == null)
		{
			return null;
		}

		return _accessor.Context.RequestHeaders.TryGetValue(headerName, out var value) ? value : null;
	}
}