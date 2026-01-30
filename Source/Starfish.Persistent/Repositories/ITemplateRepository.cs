using Nerosoft.Starfish.Persistent.Data;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Persistent.Repositories;

internal interface ITemplateRepository : IRepository
{
	Task<long> SaveAsync(TemplateData data, CancellationToken cancellationToken = default);

	Task<TemplateData> GetAsync(long id, CancellationToken cancellationToken = default);

	Task<bool> ExistsDefaultAsync(string code, TemplateType type, long excludeId = 0, CancellationToken cancellationToken = default);

	Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}