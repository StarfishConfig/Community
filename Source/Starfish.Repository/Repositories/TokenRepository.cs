using Microsoft.EntityFrameworkCore;
using Nerosoft.Euonia.Linq;
using Nerosoft.Euonia.Mapping;
using Nerosoft.Starfish.Domain.Repositories;
using Nerosoft.Starfish.Persistent;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Repository.Specifications;

namespace Nerosoft.Starfish.Repository.Repositories;

internal class TokenRepository(IdentityDataContext context) : ITokenRepository, ITransientDependency
{
	public async Task SaveAsync(TokenData data, CancellationToken cancellationToken = default)
	{
		var entity = TypeAdapter.ProjectedAs<Token>(data);
		await context.Set<Token>().AddAsync(entity, cancellationToken);
		await context.SaveChangesAsync(true, cancellationToken);
	}

	public async Task DeleteAsync(string type, string key, CancellationToken cancellationToken = default)
	{
		var specification = new CompositeSpecification<Token>(
			PredicateOperator.AndAlso,
			TokenSpecification.TypeEquals(type), TokenSpecification.KeyEquals(key)
		);

		var predicate = specification.Satisfy();

		var entity = await context.Set<Token>().FirstOrDefaultAsync(predicate, cancellationToken);
		if (entity == null)
		{
			throw new NotFoundException($"No token found with the specified key: {key}");
		}

		context.Set<Token>().Remove(entity);
		await context.SaveChangesAsync(true, cancellationToken);
	}
}