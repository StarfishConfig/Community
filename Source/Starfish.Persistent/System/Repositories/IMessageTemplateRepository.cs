using Nerosoft.Starfish.Persistent.Data;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Persistent.Repositories;

internal interface IMessageTemplateRepository
{
	Task<long> SaveAsync(MessageTemplateData data, CancellationToken cancellationToken = default);

	Task<MessageTemplateData> GetAsync(long id, CancellationToken cancellationToken = default);

	Task<bool> ExistsDefaultAsync(string code, MessageTemplateType type, long excludeId = 0, CancellationToken cancellationToken = default);

	Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}