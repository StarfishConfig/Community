using Microsoft.EntityFrameworkCore;
using Nerosoft.Euonia.Mapping;
using Nerosoft.Starfish.Persistent.Data;
using Nerosoft.Starfish.Persistent.Repositories;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Repositories;

internal class OnetimePasswordRepository(IdentityDataContext context) : IOnetimePasswordRepository
{
	public async Task<OnetimePasswordData> GetAsync(string requestId, CancellationToken cancellationToken = default)
	{
		var entity = await context.Set<OnetimePassword>()
			.FirstOrDefaultAsync(t => t.RequestId == requestId, cancellationToken);
		return TypeAdapter.ProjectedAs<OnetimePasswordData>(entity);
	}

	public async Task SaveAsync(OnetimePasswordData data, CancellationToken cancellationToken = default)
	{
		var entity = TypeAdapter.ProjectedAs<OnetimePassword>(data);
		await context.AddAsync(entity, cancellationToken);
		await context.SaveChangesAsync(true, cancellationToken);
	}

	public Task CheckOffAsync(string requestId, DateTime checkedAt, CancellationToken cancellationToken = default)
	{
		return context.Set<OnetimePassword>()
			.Where(t => t.RequestId == requestId)
			.ExecuteUpdateAsync(t => t.SetProperty(p => p.Checked, checkedAt), cancellationToken);
	}
}
