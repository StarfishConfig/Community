using Nerosoft.Euonia.Mapping;
using Nerosoft.Starfish.Domain.Repositories;
using Nerosoft.Starfish.Persistent;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Repositories;

internal class AuthlogRepository(IdentityDataContext context) : IAuthlogRepository, ITransientDependency
{
	public async Task SaveAsync(AuthlogData data, CancellationToken cancellationToken = default)
	{
		var entity = TypeAdapter.ProjectedAs<Authlog>(data);
		await context.Set<Authlog>().AddAsync(entity, cancellationToken);
		await context.SaveChangesAsync(true, cancellationToken);
	}
}