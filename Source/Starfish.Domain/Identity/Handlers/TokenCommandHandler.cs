using Nerosoft.Euonia.Bus;
using Nerosoft.Euonia.Business;
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
		return ExecuteAsync(async () =>
		{
			var aggregate = await Factory.CreateAsync<Token>(message, cancellationToken);
			aggregate.MarkAsNew();
			await aggregate.SaveAsync(false, cancellationToken);
		}, cancellationToken);
	}

	public Task HandleAsync(TokenDeleteCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return ExecuteAsync(async () =>
		{
			await Factory.DeleteAsync<Token>(message, cancellationToken);
		}, cancellationToken);
	}
}