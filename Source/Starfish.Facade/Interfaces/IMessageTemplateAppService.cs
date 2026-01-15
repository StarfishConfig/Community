using Nerosoft.Euonia.Application;
using Nerosoft.Starfish.Facade.Transit;

namespace Nerosoft.Starfish.Facade.Interfaces;

public interface IMessageTemplateAppService : IApplicationService
{
	Task<MessageTemplateDetailDto> GetAsync(string id, CancellationToken cancellationToken = default);

	Task<List<MessageTemplateListDto>> QueryAsync(MessageTemplateCriteria criteria, int skip, int size, CancellationToken cancellationToken = default);

	Task<int> CountAsync(MessageTemplateCriteria criteria, CancellationToken cancellationToken = default);

	Task<string> CreateAsync(MessageTemplateEditDto dto, CancellationToken cancellationToken = default);

	Task UpdateAsync(string id, MessageTemplateEditDto dto, CancellationToken cancellationToken = default);

	Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}
