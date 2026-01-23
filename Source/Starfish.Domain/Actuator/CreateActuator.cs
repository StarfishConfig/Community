using Nerosoft.Euonia.Business;

namespace Nerosoft.Starfish.Domain;

internal class CreateActuator<TTarget> : ActuatorBase<TTarget>
	where TTarget : EditableObject<TTarget>
{
	public CreateActuator(ActuatorBuilder<TTarget> builder, Func<Task<TTarget>> factory)
		: base(builder, factory)
	{
	}

	protected override Task ContinueHandleAsync(TTarget target, CancellationToken cancellationToken = default)
	{
		target.MarkAsNew();
		return base.ContinueHandleAsync(target, cancellationToken);
	}
}
