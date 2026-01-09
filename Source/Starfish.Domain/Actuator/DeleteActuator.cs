using Nerosoft.Euonia.Business;

namespace Nerosoft.Starfish.Domain;

internal class DeleteActuator<TTarget> : ActuatorBase<TTarget>
	where TTarget : EditableObject<TTarget>
{
	public DeleteActuator(ActuatorBuilder<TTarget> builder, Func<Task<TTarget>> factory)
		: base(builder, factory)
	{
	}

	protected override async Task ContinueHandleAsync(TTarget target, CancellationToken cancellationToken = default)
	{
		target.MarkAsDeleted();
	}
}
