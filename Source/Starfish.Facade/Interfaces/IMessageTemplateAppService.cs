using Nerosoft.Euonia.Application;
using Nerosoft.Starfish.Facade.Transit;

namespace Nerosoft.Starfish.Facade.Interfaces;

public interface IMessageTemplateAppService : IApplicationService
{
	Task<MessageTemplateDetailDto> GetAsync(long id, CancellationToken cancellationToken = default);

	Task<List<MessageTemplateListDto>> QueryAsync(MessageTemplateCriteria criteria, int skip, int size, CancellationToken cancellationToken = default);

	Task<int> CountAsync(MessageTemplateCriteria criteria, CancellationToken cancellationToken = default);

	Task<long> CreateAsync(MessageTemplateEditDto dto, CancellationToken cancellationToken = default);

	Task UpdateAsync(long id, MessageTemplateEditDto dto, CancellationToken cancellationToken = default);

	Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}