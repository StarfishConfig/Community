using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Nerosoft.Euonia.Business;
using Nerosoft.Euonia.Uow;

namespace Nerosoft.Starfish.Domain;

internal class Actuator(IObjectFactory factory, IUnitOfWorkManager unitOfWork)
{
	public ActuatorBuilder<TTarget> For<TTarget>()
		where TTarget : EditableObject<TTarget>
	{
		return new ActuatorBuilder<TTarget>(factory, unitOfWork);
	}

	public static ActuatorBuilder<TTarget> For<TTarget>([NotNull] IObjectFactory factory, IUnitOfWorkManager unitOfWork)
		where TTarget : EditableObject<TTarget>
	{
		return new ActuatorBuilder<TTarget>(factory, unitOfWork);
	}

	public static ActuatorBuilder<TTarget> For<TTarget>([NotNull] IServiceProvider provider)
		where TTarget : EditableObject<TTarget>
	{
		var factory = provider.GetRequiredService<IObjectFactory>();
		var unitOfWork = provider.GetService<IUnitOfWorkManager>();

		return new ActuatorBuilder<TTarget>(factory!, unitOfWork!);
	}
}