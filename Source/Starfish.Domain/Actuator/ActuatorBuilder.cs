using Nerosoft.Euonia.Osba;
using Nerosoft.Euonia.Uow;

namespace Nerosoft.Starfish.Domain;

internal class ActuatorBuilder<TTarget>(IObjectFactory factory, IUnitOfWorkManager unitOfWork)
	where TTarget : EditableObject<TTarget>
{
	public bool UnitOfWorkEnabled { get; private set; } = true;

	public bool Transactional { get; private set; }

	public IUnitOfWorkManager UnitOfWorkManager => unitOfWork;

	public ActuatorBuilder<TTarget> UnitOfWork(bool enabled = true, bool transactional = false)
	{
		UnitOfWorkEnabled = enabled;
		Transactional = transactional;
		return this;
	}

	public FetchActuator<TTarget> Fetch(params object[] criteria)
	{
		return new FetchActuator<TTarget>(this, () => factory.FetchAsync<TTarget>(criteria));
	}

	public CreateActuator<TTarget> Create(params object[] criteria)
	{
		return new CreateActuator<TTarget>(this, () => factory.CreateAsync<TTarget>(criteria));
	}

	public DeleteActuator<TTarget> Delete(params object[] criteria)
	{
		return new DeleteActuator<TTarget>(this, () => factory.FetchAsync<TTarget>(criteria));
	}
}