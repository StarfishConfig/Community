using Nerosoft.Euonia.Application;
using Nerosoft.Euonia.Mapping;
using Nerosoft.Starfish.Facade.Interfaces;
using Nerosoft.Starfish.Repository.Models;
using Nerosoft.Starfish.Repository.Requests;
using Nerosoft.Starfish.Transit;

namespace Nerosoft.Starfish.Facade.Implements;

internal class AuthlogApplicationService : BaseApplicationService, IAuthlogApplicationService
{
	public Task<List<AuthlogListDto>> SearchAsync(AuthlogCriteriaDto criteria, int skip, int size, CancellationToken cancellationToken = default)
	{
		var model = TypeAdapter.ProjectedAs<AuthlogCriteriaModel>(criteria);

		var request = new AuthlogSearchQuery(model, skip, size);

		return Bus.CallAsync(request, cancellationToken)
			.ContinueWith(task =>
			{
				task.WaitAndUnwrapException(cancellationToken);
				return TypeAdapter.ProjectedAs<List<AuthlogListDto>>(task.Result);
			});
	}

	public Task<int> CountAsync(AuthlogCriteriaDto criteria, CancellationToken cancellationToken = default)
	{
		var model = TypeAdapter.ProjectedAs<AuthlogCriteriaModel>(criteria);
		var request = new AuthlogCountQuery(model);
		return Bus.CallAsync(request, cancellationToken);
	}
}
