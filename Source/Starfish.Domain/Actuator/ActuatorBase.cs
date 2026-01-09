using Nerosoft.Euonia.Bus;
using Nerosoft.Euonia.Business;
using Nerosoft.Euonia.Domain;
using Nerosoft.Euonia.Modularity;

namespace Nerosoft.Starfish.Domain;

internal abstract class ActuatorBase<TTarget>
	where TTarget : EditableObject<TTarget>
{
	protected ActuatorBase(ActuatorBuilder<TTarget> builder, Func<Task<TTarget>> factory)
	{
		Builder = builder;
		Factory = factory;
	}

	private ActuatorBuilder<TTarget> Builder { get; set; }

	protected Func<Task<TTarget>> Factory { get; set; }

	protected Func<TTarget, Task> Handler { get; set; }

	protected virtual Task ContinueHandleAsync(TTarget target, CancellationToken cancellationToken = default)
	{
		return Task.CompletedTask;
	}

	public virtual ActuatorBase<TTarget> HandleAsync(Func<TTarget, Task> action)
	{
		// Here you would implement the logic to handle the action with the created or fetched object.
		// This is a placeholder for demonstration purposes.
		Handler = action;
		return this;
	}

	public virtual ActuatorBase<TTarget> Handle(Action<TTarget> action)
	{
		Handler = target =>
		{
			action(target);
			return Task.CompletedTask;
		};
		return this;
	}

	public async Task<TTarget> ExecuteAsync(CancellationToken cancellationToken = default)
	{
		var target = await Factory();

		if (Builder.UnitOfWorkManager != null && Builder.UnitOfWorkEnabled)
		{
			using var uow = Builder.UnitOfWorkManager.Begin(isTransactional: Builder.Transactional);
			if (Handler != null)
			{
				await Handler(target);
			}
			await ContinueHandleAsync(target, cancellationToken);
			await target.SaveAsync(target.IsChanged, cancellationToken);
			await uow.CompleteAsync(cancellationToken);
		}
		else
		{
			if (Handler != null)
			{
				await Handler(target);
			}
			await ContinueHandleAsync(target, cancellationToken);
			await target.SaveAsync(target.IsChanged, cancellationToken);
		}

		if (target is IHasDomainEvents domain)
		{
			var events = domain.GetEvents();
			if (events?.Any() == true)
			{
				var bus = target.BusinessContext.GetService<IBus>();
				var request = target.BusinessContext.GetService<IRequestContextAccessor>();
				var options = new PublishOptions
				{
					RequestTraceId = request?.Context?.TraceIdentifier
				};
				foreach (var @event in events)
				{
					await bus.PublishAsync(@event, null, options, null, cancellationToken);
				}
			}
		}

		return target;
	}
}
