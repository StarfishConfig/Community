using Microsoft.EntityFrameworkCore;
using Nerosoft.Euonia.Mapping;
using Nerosoft.Starfish.Domain.Repositories;
using Nerosoft.Starfish.Persistent;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Repositories;

internal class TeamRepository(IdentityDataContext context)
	: ITeamRepository, ITransientDependency
{
	public async Task SaveAsync(TeamData data, CancellationToken cancellationToken = default)
	{
		if (data.Id == 0)
		{
			var entity = TypeAdapter.ProjectedAs<Team>(data);
			await context.AddAsync(entity, cancellationToken);
		}
		else
		{
			var entity = await context.FindAsync<Team>([data.Id], cancellationToken);
			if (entity != null)
			{
				TypeAdapter.ProjectedAs(data, entity);
				context.Update(entity);
			}
		}

		await context.SaveChangesAsync(true, cancellationToken);
	}

	public async Task<TeamData> GetAsync(long id, CancellationToken cancellationToken = default)
	{
		var entity = await context.FindAsync<Team>([id], cancellationToken);
		if (entity == null)
		{
			throw new NotFoundException($"Team with id '{id}' was not found.");
		}

		{
		}

		return TypeAdapter.ProjectedAs<TeamData>(entity);
	}

	public Task DeleteAsync(long id, CancellationToken cancellationToken = default)
	{
		return context.Set<Team>()
		              .Where(x => x.Id == id)
		              .ExecuteDeleteAsync(cancellationToken);
	}
}