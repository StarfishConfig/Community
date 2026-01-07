using Duende.IdentityModel;
using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Domain.Commands;
using Nerosoft.Starfish.Facade.Events;

namespace Nerosoft.Starfish.Facade.Subscribers;

internal sealed class TokenEventSubscriber : IHandler<UserAuthSuccessEvent>, IHandler<TokenRefreshedEvent>
{
	private readonly IBus _bus;

	/// <summary>
	/// Initializes a new instance of the <see cref="TokenEventSubscriber"/> class.
	/// </summary>
	/// <param name="bus"></param>
	public TokenEventSubscriber(IBus bus)
	{
		_bus = bus;
	}

	public Task HandleAsync(UserAuthSuccessEvent message, MessageContext context, CancellationToken cancellationToken = default)
	{
		var command = new TokenCreateCommand
		{
			Type = OidcConstants.TokenTypes.RefreshToken,
			Token = message.RefreshToken,
			Issues = message.TokenIssueTime,
			Subject = message.UserId,
			Expires = message.TokenIssueTime.AddDays(180)
		};
		return _bus.SendAsync(command, null, cancellationToken);
	}

	public Task HandleAsync(TokenRefreshedEvent message, MessageContext context, CancellationToken cancellationToken = default)
	{
		var command = new TokenDeleteCommand
		{
			Type = OidcConstants.TokenTypes.RefreshToken,
			Token = message.OriginToken
		};

		return _bus.SendAsync(command, null, cancellationToken);
	}
}