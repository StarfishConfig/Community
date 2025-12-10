using Microsoft.Extensions.DependencyInjection;
using Nerosoft.Euonia.Application;
using Nerosoft.Euonia.Modularity;
using Nerosoft.Starfish.Business;
using Nerosoft.Starfish.Domain;
using Nerosoft.Starfish.Persist;

namespace Nerosoft.Starfish.Facade;

/// <summary>
/// Facade service module for the Linkyou application.
/// </summary>
/// <remarks>
/// <para>
/// This module participates in the application's modular initialization and service
/// registration pipeline by inheriting from <see cref="ModuleContextBase"/>.
/// </para>
/// Responsibilities:
/// <para>- Act as a central registration point for application services.</para>
/// <para>- Provide a place to add initialization logic related to application functionality.</para>
/// Dependencies:
/// <para>- Depends on <see cref="ApplicationModule"/>, 
///   <see cref="PersistServiceModule"/>, <see cref="BusinessServiceModule"/>,
///   and <see cref="DomainServiceModule"/> to ensure required services are registered
///   before this module initializes.
/// </para>
/// Extension guidance:
/// <para>- Add additional service registrations here or override lifecycle methods from
///   <see cref="ModuleContextBase"/> (such as startup/shutdown hooks) when application functionality requires initialization or configuration.
/// </para>
/// </remarks>
/// <seealso cref="ModuleContextBase"/>
[DependsOn(typeof(ApplicationModule))]
[DependsOn(typeof(PersistServiceModule), typeof(BusinessServiceModule), typeof(DomainServiceModule))]
public class FacadeServiceModule : ModuleContextBase
{
	/// <inheritdoc/>
	public override void AheadConfigureServices(ServiceConfigurationContext context)
	{
		Singleton<ServiceBusHandlerRegistration>.Get(() =>
		{
			var registration = new ServiceBusHandlerRegistration();
			registration.AddAssembly(typeof(FacadeServiceModule).Assembly);
			registration.AddAssembly(typeof(BusinessServiceModule).Assembly);
			registration.AddAssembly(typeof(PersistServiceModule).Assembly);
			return registration;
		});
	}

	/// <inheritdoc/>
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		context.Services.Register<FacadeServiceContext>();

		context.Services.AddServiceBus(Configuration);
	}
}
