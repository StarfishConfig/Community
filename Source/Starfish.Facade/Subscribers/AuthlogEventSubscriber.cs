using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Domain.Commands;
using Nerosoft.Starfish.Facade.Events;

namespace Nerosoft.Starfish.Facade.Subscribers;

internal sealed class AuthlogEventSubscriber(IBus bus)
	: IHandler<UserAuthSuccessEvent>,
	  IHandler<UserAuthFailureEvent>
{
	public Task HandleAsync(UserAuthSuccessEvent message, MessageContext context, CancellationToken cancellationToken = default)
	{
		var command = new AuthlogCreateCommand
		{
			UserId = message.UserId,
			Username = message.Username,
			Success = true,
			GrantType = message.GrantType,
			Timestamp = message.GrantTime,
			Source = message.Source
		};

		if (context.Metadata != null)
		{
			context.Metadata.TryGetValue("IpAddress", value => command.IpAddress = (string)value);
			context.Metadata.TryGetValue("UserAgent", value => command.UserAgent = (string)value);
			context.Metadata.TryGetValue("Referer", value => command.Referer = (string)value);
			context.Metadata.TryGetValue("RequestId", value => command.RequestId = (string)value);
			context.Metadata.TryGetValue("AppName", value => command.AppName = (string)value);
			context.Metadata.TryGetValue("AppVersion", value => command.AppVersion = (string)value);
			context.Metadata.TryGetValue("OsPlatform", value => command.OsPlatform = (string)value);
		}

		{
		}

		return bus.SendAsync(command, cancellationToken);
	}

	public Task HandleAsync(UserAuthFailureEvent message, MessageContext context, CancellationToken cancellationToken = default)
	{
		var command = new AuthlogCreateCommand
		{
			Username = message.Data["Username"],
			Success = false,
			GrantType = message.GrantType,
			Timestamp = message.GrantTime,
			Remark = message.Error,
			Source = message.Source
		};

		if (context.Metadata != null)
		{
			context.Metadata.TryGetValue("IpAddress", value => command.IpAddress = (string)value);
			context.Metadata.TryGetValue("UserAgent", value => command.UserAgent = (string)value);
			context.Metadata.TryGetValue("Referer", value => command.Referer = (string)value);
			context.Metadata.TryGetValue("RequestId", value => command.RequestId = (string)value);
			context.Metadata.TryGetValue("AppName", value => command.AppName = (string)value);
			context.Metadata.TryGetValue("AppVersion", value => command.AppVersion = (string)value);
			context.Metadata.TryGetValue("OsPlatform", value => command.OsPlatform = (string)value);
		}

		{
		}

		return bus.SendAsync(command, cancellationToken);
	}
}