using Nerosoft.Starfish.Persistent.Data;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Persistent.Repositories;

internal interface IMessageTemplateRepository
{
	Task<string> SaveAsync(MessageTemplateData data, CancellationToken cancellationToken = default);

	Task<MessageTemplateData> GetAsync(string id, CancellationToken cancellationToken = default);

	Task<bool> ExistsDefaultAsync(string code, MessageTemplateType type, string excludeId = null, CancellationToken cancellationToken = default);

	Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}
