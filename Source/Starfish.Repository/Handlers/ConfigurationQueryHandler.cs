using Microsoft.EntityFrameworkCore;
using Nerosoft.Euonia.Bus;
using Nerosoft.Euonia.Linq;
using Nerosoft.Euonia.Mapping;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Repository.Models;
using Nerosoft.Starfish.Repository.Requests;
using Nerosoft.Starfish.Repository.Specifications;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Handlers;

internal class ConfigurationQueryHandler : IHandler<ConfigurationBaseInfoQuery, ConfigurationBaseInfoModel>,
										   IHandler<ConfigurationDetailQuery, ConfigurationDetailModel>,
										   IHandler<ConfigurationSearchQuery, IList<ConfigurationListModel>>,
										   IHandler<ConfigurationCountQuery, int>,
										   IHandler<ConfigurationPermissionGrantQuery, IReadOnlyDictionary<long, ConfigurationPermissionGrantState>>
{
	private readonly ProjectDataContext _context;

	public ConfigurationQueryHandler(ProjectDataContext context)
	{
		_context = context;
	}

	public async Task<ConfigurationBaseInfoModel> HandleAsync(ConfigurationBaseInfoQuery message, MessageContext context, CancellationToken cancellationToken = default)
	{
		var specification = ConfigurationSpecification.IdEquals(message.Id);

		if (message.TeamScope?.Count > 0)
		{
			specification &= ConfigurationSpecification.TeamIdIn(message.TeamScope);
		}

		var predicate = specification.Satisfy();

		var entity = await _context.Set<Configuration>().FirstOrDefaultAsync(predicate, cancellationToken);

		return TypeAdapter.ProjectedAs<ConfigurationBaseInfoModel>(entity);
	}

	public async Task<ConfigurationDetailModel> HandleAsync(ConfigurationDetailQuery message, MessageContext context, CancellationToken cancellationToken = default)
	{
		var specification = ConfigurationSpecification.IdEquals(message.Id);

		if (message.TeamScope?.Count > 0)
		{
			specification &= ConfigurationSpecification.TeamIdIn(message.TeamScope);
		}

		var predicate = specification.Satisfy();

		var entity = await _context.Set<Configuration>()
							 .FirstOrDefaultAsync(predicate, cancellationToken);

		return TypeAdapter.ProjectedAs<ConfigurationDetailModel>(entity);
	}

	public async Task<IList<ConfigurationListModel>> HandleAsync(ConfigurationSearchQuery message, MessageContext context, CancellationToken cancellationToken = default)
	{
		if (message.TeamScope?.Count > 0 && message.TeamId > 0 && message.TeamScope.Contains(message.TeamId))
		{
			return [];
		}

		var predicate = ConfigurationSpecification.All
												  .AndIf(message.TeamId > 0, () => ConfigurationSpecification.TeamIdEquals(message.TeamId))
												  .AndIf(message.TeamScope?.Count > 0, () => ConfigurationSpecification.TeamIdIn(message.TeamScope))
												  .AndIf(!string.IsNullOrWhiteSpace(message.Keyword), () => ConfigurationSpecification.NameContains(message.Keyword))
												  .Satisfy();

		var query = _context.Set<Configuration>().AsNoTracking().Where(predicate)
							.OrderByDescending(t => t.Id)
							.Skip(message.Skip)
							.Take(message.Size);

		var entities = await query.ToListAsync(cancellationToken);
		return TypeAdapter.ProjectedAs<IList<ConfigurationListModel>>(entities);
	}

	public Task<int> HandleAsync(ConfigurationCountQuery message, MessageContext context, CancellationToken cancellationToken = default)
	{
		if (message.TeamScope?.Count > 0 && message.TeamId > 0 && message.TeamScope.Contains(message.TeamId))
		{
			return Task.FromResult(0);
		}

		var predicate = ConfigurationSpecification.All
												  .AndIf(message.TeamId > 0, () => ConfigurationSpecification.TeamIdEquals(message.TeamId))
												  .AndIf(message.TeamScope?.Count > 0, () => ConfigurationSpecification.TeamIdIn(message.TeamScope))
												  .AndIf(!string.IsNullOrWhiteSpace(message.Keyword), () => ConfigurationSpecification.NameContains(message.Keyword))
												  .Satisfy();

		var query = _context.Set<Configuration>().AsNoTracking().Where(predicate);
		return query.CountAsync(cancellationToken);
	}

	public async Task<IReadOnlyDictionary<long, ConfigurationPermissionGrantState>> HandleAsync(ConfigurationPermissionGrantQuery message, MessageContext context, CancellationToken cancellationToken = new CancellationToken())
	{
		var entities = await _context.Set<ConfigurationPermission>().AsNoTracking()
									 .Where(t => t.ConfigurationId == message.ConfigurationId && t.UserId == message.UserId)
									 .ToListAsync(cancellationToken);

		var result = new Dictionary<long, ConfigurationPermissionGrantState>();

		foreach (var entity in entities)
		{
			var grantState = ConfigurationPermissionGrantState.None;

			if (entity.Read)
			{
				grantState |= ConfigurationPermissionGrantState.Read;
			}

			if (entity.Write)
			{
				grantState |= ConfigurationPermissionGrantState.Write;
			}

			if (entity.Publish)
			{
				grantState |= ConfigurationPermissionGrantState.Publish;
			}

			result[entity.ConfigurationId] = grantState;
		}

		return result;
	}
}