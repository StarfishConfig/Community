using Microsoft.EntityFrameworkCore;
using Nerosoft.Euonia.Bus;
using Nerosoft.Euonia.Linq;
using Nerosoft.Euonia.Mapping;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Repository.Models;
using Nerosoft.Starfish.Repository.Requests;
using Nerosoft.Starfish.Repository.Specifications;

namespace Nerosoft.Starfish.Repository.Handlers;

internal class ConfigurationQueryHandler : IHandler<ConfigurationBaseInfoQuery, ConfigurationBaseInfoModel>,
                                           IHandler<ConfigurationDetailQuery, ConfigurationDetailModel>,
                                           IHandler<ConfigurationSearchQuery, IList<ConfigurationListModel>>,
                                           IHandler<ConfigurationCountQuery, int>
{
	private readonly ConfigsDataContext _context;

	public ConfigurationQueryHandler(ConfigsDataContext context)
	{
		_context = context;
	}

	public async Task<ConfigurationBaseInfoModel> HandleAsync(ConfigurationBaseInfoQuery message, MessageContext context, CancellationToken cancellationToken = default)
	{
		if (message.TeamScope?.Count > 0 && message.Id > 0 && message.TeamScope.Contains(message.Id))
		{
			return null;
		}

		var entity = await _context.Set<Configuration>().FindAsync([message.Id], cancellationToken);

		return TypeAdapter.ProjectedAs<ConfigurationBaseInfoModel>(entity);
	}

	public async Task<ConfigurationDetailModel> HandleAsync(ConfigurationDetailQuery message, MessageContext context, CancellationToken cancellationToken = default)
	{
		if (message.TeamScope?.Count > 0 && message.Id > 0 && message.TeamScope.Contains(message.Id))
		{
			return null;
		}

		var entity = await _context.Set<Configuration>().FindAsync([message.Id], cancellationToken);

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
		                                          .AndIf(!string.IsNullOrWhiteSpace(message.Keyword), () => ConfigurationSpecification.NameContains(message.Keyword))
		                                          .Satisfy();

		var query = _context.Set<Configuration>().AsNoTracking().Where(predicate);
		return query.CountAsync(cancellationToken);
	}
}