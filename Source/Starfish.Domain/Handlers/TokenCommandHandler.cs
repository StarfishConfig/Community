using Nerosoft.Euonia.Bus;
using Nerosoft.Euonia.Osba;
using Nerosoft.Euonia.Uow;
using Nerosoft.Starfish.Domain.Aggregates;
using Nerosoft.Starfish.Domain.Commands;

namespace Nerosoft.Starfish.Domain.Handlers;

internal sealed class TokenCommandHandler(IUnitOfWorkManager unitOfWork, IObjectFactory factory)
	: CommandHandlerBase(unitOfWork, factory),
	  IHandler<TokenCreateCommand>,
	  IHandler<TokenDeleteCommand>
{
	public Task HandleAsync(TokenCreateCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<Token>()
				 .Create(message, cancellationToken)
				 .HandleAsync(async target => target.MarkAsNew())
				 .ExecuteAsync(cancellationToken);
	}

	public Task HandleAsync(TokenDeleteCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		//return ExecuteAsync(async () =>
		//{
		//	await Factory.DeleteAsync<Token>(message, cancellationToken);
		//}, cancellationToken);

		return Actuator.For<Token>()
					 .Delete(message, cancellationToken)
					 .ExecuteAsync(cancellationToken);
	}
}