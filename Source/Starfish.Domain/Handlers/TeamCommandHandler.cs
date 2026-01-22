using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Domain.Aggregates;
using Nerosoft.Starfish.Domain.Commands;

namespace Nerosoft.Starfish.Domain.Handlers;

internal class TeamCommandHandler(IServiceProvider provider)
	: CommandHandlerBase(provider),
	  IHandler<TeamCreateCommand>,
	  IHandler<TeamUpdateCommand>,
	  IHandler<TeamDeleteCommand>,
	  IHandler<TeamTransferCommand>,
	  IHandler<TeamMemberAppendCommand>,
	  IHandler<TeamMemberRemoveCommand>
{
	public Task HandleAsync(TeamCreateCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<Team>()
		               .Create(message.Name, cancellationToken)
		               .Handle(target =>
		               {
			               target.SetDescription(message.Description);
			               target.MarkAsNew();
		               })
		               .ExecuteAsync(cancellationToken);
	}

	public Task HandleAsync(TeamUpdateCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<Team>()
		               .Fetch(message.Id, cancellationToken)
		               .Handle(target =>
		               {
			               target.SetName(message.Name);
			               target.SetDescription(message.Description);
			               target.MarkAsChanged();
		               })
		               .ExecuteAsync(cancellationToken);
	}

	public Task HandleAsync(TeamDeleteCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<Team>()
		               .Fetch(message.Id, cancellationToken)
		               .Handle(target =>
		               {
			               target.MarkAsDeleted();
		               })
		               .ExecuteAsync(cancellationToken);
	}

	public Task HandleAsync(TeamTransferCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<Team>()
		               .Fetch(message.Id, cancellationToken)
		               .Handle(target =>
		               {
			               target.Transfer(message.OwnerId, message.LeaveAfterTransfer);
			               target.MarkAsChanged();
		               })
		               .ExecuteAsync(cancellationToken);
	}

	public Task HandleAsync(TeamMemberAppendCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<Team>()
		               .Fetch(message.Id, cancellationToken)
		               .Handle(target =>
		               {
			               target.AppendMember(message.UserIds?.ToArray());
			               target.MarkAsChanged();
		               })
		               .ExecuteAsync(cancellationToken);
	}

	public Task HandleAsync(TeamMemberRemoveCommand message, MessageContext context, CancellationToken cancellationToken = default)
	{
		return Actuator.For<Team>()
		               .Fetch(message.Id, cancellationToken)
		               .Handle(target =>
		               {
			               target.RemoveMember(message.UserIds?.ToArray());
			               target.MarkAsChanged();
		               })
		               .ExecuteAsync(cancellationToken);
	}
}