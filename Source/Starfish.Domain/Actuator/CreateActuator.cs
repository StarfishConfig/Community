using Nerosoft.Euonia.Business;

namespace Nerosoft.Starfish.Domain;

internal class CreateActuator<TTarget> : ActuatorBase<TTarget>
	where TTarget : EditableObject<TTarget>
{
	public CreateActuator(ActuatorBuilder<TTarget> builder, Func<Task<TTarget>> factory)
		: base(builder, factory)
	{
	}
}
