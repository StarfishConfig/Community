using Microsoft.Extensions.DependencyInjection;
using Nerosoft.Euonia.Application;
using Nerosoft.Euonia.Bus;
using Nerosoft.Euonia.Bus.InMemory;
using Nerosoft.Euonia.Bus.RabbitMq;
using Nerosoft.Euonia.Modularity;
using Nerosoft.Euonia.Uow;
using Nerosoft.Starfish.Domain;
using Nerosoft.Starfish.Facade.Auth;
using Nerosoft.Starfish.Repository;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Facade;

/// <summary>
/// Facade service module for the Starfish application.
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
/// <para>- Depends on <see cref="ApplicationModule"/>, <see cref="UnitOfWorkModule"/>, <see cref="RepositoryServiceModule"/>,
///   and <see cref="DomainServiceModule"/> to ensure required services are registered
///   before this module initializes.
/// </para>
/// Extension guidance:
/// <para>- Add additional service registrations here or override lifecycle methods from
///   <see cref="ModuleContextBase"/> (such as startup/shutdown hooks) when application functionality requires initialization or configuration.
/// </para>
/// </remarks>
/// <seealso cref="ModuleContextBase"/>
[DependsOn(typeof(ApplicationModule), typeof(UnitOfWorkModule))]
[DependsOn(typeof(RepositoryServiceModule), typeof(DomainServiceModule))]
[DependsOn(typeof(InMemoryBusModule), typeof(RabbitMqBusModule))]
public class FacadeServiceModule : ModuleContextBase
{
	/// <inheritdoc/>
	public override void AheadConfigureServices(ServiceConfigurationContext context)
	{
		Singleton<MessageBusHandlerRegistration>.Get(() =>
		{
			var registration = new MessageBusHandlerRegistration();
			registration.AddAssembly(typeof(FacadeServiceModule).Assembly);
			registration.AddAssembly(typeof(DomainServiceModule).Assembly);
			registration.AddAssembly(typeof(RepositoryServiceModule).Assembly);
			return registration;
		});
	}

	/// <summary>
	/// Adds and configures the service bus to the service collection.
	/// </summary>
	/// <param name="context"></param>
	/// <exception cref="InvalidOperationException">Thrown when no service bus handler assemblies are registered.</exception>
	/// <remarks>
	/// This method configures the service bus with the following:
	/// <list type="bullet">
	/// <item><description>Message conventions for routing and handling.</description></item>
	/// <item><description>Identity provider using JWT for authentication.</description></item>
	/// <item><description>Handler registration from specified assemblies.</description></item>
	/// <item><description>Provider-specific configuration based on the ServiceBus:Provider setting.</description></item>
	/// </list>
	/// Supported providers: InMemory, RabbitMq.
	/// </remarks>
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		context.Services.Register<FacadeServiceContext>();

		context.Services.AddEuoniaBus(config =>
		{
			var registration = Singleton<MessageBusHandlerRegistration>.Instance;
			if (registration == null || registration.Assemblies.Count == 0)
			{
				throw new InvalidOperationException("No service bus handler assemblies registered.");
			}

			config.RegisterHandlers(registration.Assemblies.ToArray())
			      .SetConventions(builder =>
			      {
				      builder.Add<DefaultMessageConvention>();
				      builder.Add<AttributeMessageConvention>();
				      builder.Add<DomainMessageConvention>();
			      })
			      .SetStrategy(MessageBusProvider.InMemory, builder =>
			      {
				      builder.Add<LocalMessageTransportStrategy>();
				      builder.Add(new AttributeTransportStrategy([MessageBusProvider.InMemory]));
				      builder.EvaluateIncoming(_ => true);
				      builder.EvaluateOutgoing(_ => true);
			      })
			      .SetStrategy(MessageBusProvider.RabbitMq, builder =>
			      {
				      builder.Add<DistributedMessageTransportStrategy>();
				      builder.Add(new AttributeTransportStrategy([MessageBusProvider.RabbitMq]));
			      })
			      .SetIdentityProvider(jwt => JwtIdentityAccessor.Resolve(jwt, Configuration));
		});

		context.Services
		       .AddKeyedTransient<IExternalAuthProvider, GoogleAuthProvider>(AuthProvider.Google)
		       .AddKeyedTransient<IExternalAuthProvider, GithubAuthProvider>(AuthProvider.Github)
		       .AddKeyedTransient<IExternalAuthProvider, FacebookAuthProvider>(AuthProvider.Facebook)
		       .AddKeyedTransient<IExternalAuthProvider, MicrosoftAuthProvider>(AuthProvider.Microsoft);
	}
}