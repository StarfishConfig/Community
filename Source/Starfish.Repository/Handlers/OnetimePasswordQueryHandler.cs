using Microsoft.EntityFrameworkCore;
using Nerosoft.Euonia.Bus;
using Nerosoft.Euonia.Mapping;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Repository.Models;
using Nerosoft.Starfish.Repository.Requests;
using Nerosoft.Starfish.Repository.Specifications;

namespace Nerosoft.Starfish.Repository.Handlers;

internal class OnetimePasswordQueryHandler : IHandler<OnetimePasswordDetailQuery, OnetimePasswordDetailModel>
{
	private readonly IdentityDataContext _context;

	public OnetimePasswordQueryHandler(IdentityDataContext context)
	{
		_context = context;
	}

	public async Task<OnetimePasswordDetailModel> HandleAsync(OnetimePasswordDetailQuery message, MessageContext context, CancellationToken cancellationToken = default)
	{
		var predicate = OnetimePasswordSpecification.RequestIdEquals(message.RequestId).Satisfy();
		var entity = await _context.Set<OnetimePassword>().AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);
		return TypeAdapter.ProjectedAs<OnetimePasswordDetailModel>(entity);
	}
}
