using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Domain.Aggregates;
using Nerosoft.Starfish.Domain.Commands;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Domain.Handlers;

internal sealed class UserCommandHandler(IServiceProvider provider)
	: CommandHandlerBase(provider),
	IHandler<UserCreateCommand>,
	IHandler<UserUpdateCommand>,
	IHandler<UserPasswordChangeCommand>,
	IHandler<UserPasswordResetCommand>
{
	public Task HandleAsync(UserCreateCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<User>()
			.Create(message.Username, message.Password, cancellationToken)
			.Handle(user =>
			{
				user.SetPassword(message.Password, UserPasswordUpdateType.Create);
				user.SetEmail(message.Email);
				user.SetPhone(message.Phone);
				user.SetNickname(message.Nickname);
				user.AssignRoles(message.Roles.ToArray());
			})
			.ExecuteAsync(cancellationToken);
	}

	public Task HandleAsync(UserUpdateCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<User>()
			.Fetch(message.Id, cancellationToken)
			.Handle(user =>
			{
				user.SetEmail(message.Email);
				user.SetPhone(message.Phone);
				user.SetNickname(message.Nickname);
			})
			.ExecuteAsync(cancellationToken);
	}

	public Task HandleAsync(UserPasswordChangeCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<User>()
			.Fetch(message.Id, cancellationToken)
			.Handle(user =>
			{
				user.SetPassword(message.Password, UserPasswordUpdateType.Change);
			})
			.ExecuteAsync(cancellationToken);
	}

	public Task HandleAsync(UserPasswordResetCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<User>()
			.Fetch(message.Id, cancellationToken)
			.Handle(user =>
			{
				user.SetPassword(message.Password, UserPasswordUpdateType.Reset);
			})
			.ExecuteAsync(cancellationToken);
	}
}