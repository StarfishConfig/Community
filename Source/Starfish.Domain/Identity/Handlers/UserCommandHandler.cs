using Nerosoft.Euonia.Business;
using Nerosoft.Euonia.Uow;

namespace Nerosoft.Starfish.Domain.Handlers;

internal sealed class UserCommandHandler(IUnitOfWorkManager unitOfWork, IObjectFactory factory)
	: CommandHandlerBase(unitOfWork, factory)
{
}