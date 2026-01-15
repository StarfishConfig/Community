using System.Reactive.Subjects;
using Nerosoft.Euonia.Application;
using Nerosoft.Euonia.Mapping;
using Nerosoft.Starfish.Domain.Commands;
using Nerosoft.Starfish.Facade.Interfaces;
using Nerosoft.Starfish.Facade.Transit;
using Nerosoft.Starfish.Repository.Requests;

namespace Nerosoft.Starfish.Facade.Implements;

internal class MessageTemplateAppService : BaseApplicationService, IMessageTemplateAppService
{
	public Task<MessageTemplateDetailDto> GetAsync(string id, CancellationToken cancellationToken = default)
	{
		var request = new MessageTemplateDetailQuery(id);
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

			          return TypeAdapter.ProjectedAs<MessageTemplateDetailDto>(result);
		          }, cancellationToken);
	}

	public Task<List<MessageTemplateListDto>> QueryAsync(MessageTemplateCriteria criteria, int skip, int size, CancellationToken cancellationToken = default)
	{
		var request = new MessageTemplateListQuery(criteria.Code, criteria.Type, criteria.Keyword, skip, size);
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

			          return TypeAdapter.ProjectedAs<List<MessageTemplateListDto>>(result);
		          }, cancellationToken);
	}

	public Task<int> CountAsync(MessageTemplateCriteria criteria, CancellationToken cancellationToken = default)
	{
		var request = new MessageTemplateCountQuery(criteria.Code, criteria.Type, criteria.Keyword);
		return Bus.CallAsync(request, cancellationToken);
	}

	public async Task<string> CreateAsync(MessageTemplateEditDto dto, CancellationToken cancellationToken = default)
	{
		var command = TypeAdapter.ProjectedAs<MessageTemplateCreateCommand>(dto);
		var tcs = new TaskCompletionSource<string>();

		var subject = new Subject<string>();
		subject.Subscribe
		(
			id => tcs.SetResult(id),
			ex => tcs.SetException(ex),
			() => tcs.TrySetResult(null)
		);

		await Bus.SendAsync(command, subject, cancellationToken);
		return await tcs.Task;
	}

	public Task UpdateAsync(string id, MessageTemplateEditDto dto, CancellationToken cancellationToken = default)
	{
		var command = new MessageTemplateUpdateCommand(id);
		TypeAdapter.ProjectedAs(dto, command);
		return Bus.SendAsync(command, cancellationToken);
	}

	public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
	{
		var command = new MessageTemplateDeleteCommand(id);
		return Bus.SendAsync(command, cancellationToken);
	}
}