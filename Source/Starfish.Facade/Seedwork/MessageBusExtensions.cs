using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nerosoft.Euonia.Bus;

namespace Nerosoft.Starfish.Facade;

internal static class MessageBusExtensions
{
	/// <summary>
	/// Adds and configures the service bus to the service collection.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
	/// <param name="configuration">The <see cref="IConfiguration"/> instance containing service bus settings.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	/// <exception cref="InvalidOperationException">Thrown when no service bus handler assemblies are registered.</exception>
	/// <exception cref="NotSupportedException">Thrown when the specified service bus provider is not supported.</exception>
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
	public static IBusConfigurator AddServiceBus(this IServiceCollection services, IConfiguration configuration)
	{
		var bus = services.AddEuoniaBus(config =>
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
				      // builder.EvaluateIncoming(_ => true);
				      // builder.EvaluateOutgoing(_ => true);
			      })
			      .SetIdentityProvider(jwt => JwtIdentityAccessor.Resolve(jwt, configuration));
		});

		return bus;
	}
}