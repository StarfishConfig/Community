using Nerosoft.Euonia.Business;

namespace Nerosoft.Starfish.Domain;

internal class FetchActuator<TTarget> : ActuatorBase<TTarget>
		where TTarget : EditableObject<TTarget>
{
	public FetchActuator(ActuatorBuilder<TTarget> builder, Func<Task<TTarget>> factory)
		: base(builder, factory)
	{
	}
}
