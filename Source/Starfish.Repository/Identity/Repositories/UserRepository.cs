using Microsoft.EntityFrameworkCore;
using Nerosoft.Euonia.Mapping;
using Nerosoft.Starfish.Domain.Repositories;
using Nerosoft.Starfish.Persistent;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Repository.Specifications;

namespace Nerosoft.Starfish.Repository.Repositories;

internal class UserRepository(IdentityDataContext context) : IUserRepository, ITransientDependency
{
	public async Task SaveAsync(UserData data, CancellationToken cancellationToken = default)
	{
		if (string.IsNullOrWhiteSpace(data.Id))
		{
			var entity = TypeAdapter.ProjectedAs<User>(data);
			await context.Set<User>().AddAsync(entity, cancellationToken);
			await context.SaveChangesAsync(cancellationToken);
			//data.Id = entity.Id;
		}
		else
		{
			var entity = await context.Set<User>().FindAsync([data.Id], cancellationToken: cancellationToken);
			if (entity == null)
			{
				throw new InvalidOperationException("User not found.");
			}

			TypeAdapter.ProjectedAs(data, entity);

			context.Set<User>().Update(entity);
			await context.SaveChangesAsync(cancellationToken);
		}
	}

	public async Task<UserData> GetAsync(string id, CancellationToken cancellationToken = default)
	{
		var entity = await context.Set<User>().FindAsync([id], cancellationToken: cancellationToken);
		if (entity == null)
		{
			return null;
		}

		var data = TypeAdapter.ProjectedAs<UserData>(entity);
		return data;
	}

	public Task<bool> ExistsPhoneAsync(string phone, CancellationToken cancellationToken = default)
	{
		var specification = UserSpecification.PhoneEquals(phone);
		var predicate = specification.Satisfy();
		return context.Set<User>().AnyAsync(predicate, cancellationToken);
	}

	public Task<bool> ExistsEmailAsync(string email, CancellationToken cancellationToken = default)
	{
		var specification = UserSpecification.EmailEquals(email);
		var predicate = specification.Satisfy();
		return context.Set<User>().AnyAsync(predicate, cancellationToken);
	}

	public Task<bool> ExistsUsernameAsync(string username, CancellationToken cancellationToken = default)
	{
		var specification = UserSpecification.UsernameEquals(username);
		var predicate = specification.Satisfy();
		return context.Set<User>().AnyAsync(predicate, cancellationToken);
	}
}