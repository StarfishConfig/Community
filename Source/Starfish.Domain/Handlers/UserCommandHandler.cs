using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Domain.Aggregates;
using Nerosoft.Starfish.Domain.Commands;
using Nerosoft.Starfish.Infrastructure;

namespace Nerosoft.Starfish.Domain.Handlers;

internal sealed class UserCommandHandler(IServiceProvider provider)
	: CommandHandlerBase(provider),
	  IHandler<UserCreateCommand>,
	  IHandler<UserUpdateCommand>,
	  IHandler<UserPasswordChangeCommand>,
	  IHandler<UserPasswordResetCommand>,
	  IHandler<UserFailedCountChangeCommand>
{
	public Task HandleAsync(UserCreateCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<User>()
					   .Create(message.Username, cancellationToken)
					   .Handle(user =>
					   {
						   user.SetPassword(message.Password, PasswordUpdateType.Create);
						   user.SetEmail(message.Email);
						   user.SetPhone(message.Phone);
						   user.SetNickname(message.Nickname);
						   if (message.Roles != null)
						   {
							   user.AssignRoles(message.Roles.ToArray());
						   }
						   user.MarkAsNew();
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
						   user.MarkAsChanged();
					   })
					   .ExecuteAsync(cancellationToken);
	}

	public Task HandleAsync(UserPasswordChangeCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<User>()
					   .Fetch(message.Id, cancellationToken)
					   .Handle(user =>
					   {
						   user.SetPassword(message.Password, PasswordUpdateType.Change);
						   user.MarkAsChanged();
					   })
					   .ExecuteAsync(cancellationToken);
	}

	public Task HandleAsync(UserPasswordResetCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<User>()
					   .Fetch(message.Id, cancellationToken)
					   .Handle(user =>
					   {
						   user.SetPassword(message.Password, PasswordUpdateType.Reset);
						   user.MarkAsChanged();
					   })
					   .ExecuteAsync(cancellationToken);
	}

	public Task HandleAsync(UserFailedCountChangeCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<User>()
					   .Fetch(message.Id, cancellationToken)
					   .Handle(user =>
					   {
						   switch (message.ActionType)
						   {
							   case "+":
								   user.IncreaseAccessFailedCount();
								   break;
							   case "-":
								   user.ResetAccessFailedCount();
								   break;
						   }

						   user.MarkAsChanged();
					   })
					   .ExecuteAsync(cancellationToken);
	}
}