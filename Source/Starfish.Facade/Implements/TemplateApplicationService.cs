using System.Reactive.Subjects;
using Nerosoft.Euonia.Application;
using Nerosoft.Euonia.Mapping;
using Nerosoft.Starfish.Domain.Commands;
using Nerosoft.Starfish.Facade.Interfaces;
using Nerosoft.Starfish.Facade.Transit;
using Nerosoft.Starfish.Repository.Requests;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Facade.Implements;

internal class TemplateApplicationService : BaseApplicationService, ITemplateApplicationService
{
	public Task<TemplateDetailDto> GetAsync(long id, CancellationToken cancellationToken = default)
	{
		var request = new TemplateDetailQuery(id);
		return Bus.CallAsync(request, cancellationToken)
		          .ContinueWith(task =>
		          {
			          if (task.IsFaulted)
			          {
				          throw task.Exception;
			          }

			          var result = task.Result;
			          if (result == null)
			          {
				          throw new NotFoundException("Message template not found.");
			          }

			          {
			          }

			          return TypeAdapter.ProjectedAs<TemplateDetailDto>(result);
		          }, cancellationToken);
	}

	public Task<List<TemplateListDto>> SearchAsync(TemplateType type, string keyword, int skip, int size, CancellationToken cancellationToken = default)
	{
		var request = new TemplateSearchRequest(type, keyword, skip, size);
		return Bus.CallAsync(request, cancellationToken)
		          .ContinueWith(task =>
		          {
			          if (task.IsFaulted)
			          {
				          throw task.Exception;
			          }

			          var result = task.Result;
			          if (result?.Any() != true)
			          {
				          return [];
			          }

			          {
			          }

			          return TypeAdapter.ProjectedAs<List<TemplateListDto>>(result);
		          }, cancellationToken);
	}

	public Task<int> CountAsync(TemplateType type, string keyword, CancellationToken cancellationToken = default)
	{
		var request = new TemplateCountRequest(type, keyword);
		return Bus.CallAsync(request, cancellationToken);
	}

	public async Task<long> CreateAsync(TemplateEditDto dto, CancellationToken cancellationToken = default)
	{
		var command = TypeAdapter.ProjectedAs<TemplateCreateCommand>(dto);
		var tcs = new TaskCompletionSource<long>();

		var subject = new Subject<long>();
		subject.Subscribe
		(
			id => tcs.SetResult(id),
			ex => tcs.SetException(ex),
			() => tcs.TrySetResult(0)
		);

		await Bus.SendAsync(command, subject, cancellationToken);
		return await tcs.Task;
	}

	public Task UpdateAsync(long id, TemplateEditDto dto, CancellationToken cancellationToken = default)
	{
		var command = new TemplateUpdateCommand(id);
		TypeAdapter.ProjectedAs(dto, command);
		return Bus.SendAsync(command, cancellationToken);
	}

	public Task DeleteAsync(long id, CancellationToken cancellationToken = default)
	{
		var command = new TemplateDeleteCommand(id);
		return Bus.SendAsync(command, cancellationToken);
	}
}