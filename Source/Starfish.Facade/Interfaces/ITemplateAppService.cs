using Nerosoft.Euonia.Application;
using Nerosoft.Starfish.Facade.Transit;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Facade.Interfaces;

public interface ITemplateAppService : IApplicationService
{
	Task<TemplateDetailDto> GetAsync(long id, CancellationToken cancellationToken = default);

	Task<List<TemplateListDto>> SearchAsync(TemplateType type, string keyword, int skip, int size, CancellationToken cancellationToken = default);

	Task<int> CountAsync(TemplateType type, string keyword, CancellationToken cancellationToken = default);

	Task<long> CreateAsync(TemplateEditDto dto, CancellationToken cancellationToken = default);

	Task UpdateAsync(long id, TemplateEditDto dto, CancellationToken cancellationToken = default);

	Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}