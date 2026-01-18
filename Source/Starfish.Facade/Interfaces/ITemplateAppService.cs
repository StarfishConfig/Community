using Nerosoft.Euonia.Application;
using Nerosoft.Starfish.Facade.Transit;

namespace Nerosoft.Starfish.Facade.Interfaces;

public interface ITemplateAppService : IApplicationService
{
	Task<TemplateDetailDto> GetAsync(long id, CancellationToken cancellationToken = default);

	Task<List<TemplateListDto>> QueryAsync(TemplateCriteria criteria, int skip, int size, CancellationToken cancellationToken = default);

	Task<int> CountAsync(TemplateCriteria criteria, CancellationToken cancellationToken = default);

	Task<long> CreateAsync(TemplateEditDto dto, CancellationToken cancellationToken = default);

	Task UpdateAsync(long id, TemplateEditDto dto, CancellationToken cancellationToken = default);

	Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}