using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nerosoft.Euonia.Bus;
using Nerosoft.Euonia.Bus.InMemory;
using Nerosoft.Euonia.Bus.RabbitMq;

namespace Nerosoft.Starfish.Facade;

internal static class ServiceBusExtensions
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
	public static IServiceCollection AddServiceBus(this IServiceCollection services, IConfiguration configuration)
	{
		var bus = services.AddServiceBus(config =>
		{
			config.SetConventions(builder =>
			{
				builder.Add<DefaultMessageConvention>();
				builder.Add<AttributeMessageConvention>();
				builder.Add<DomainMessageConvention>();
			});
			config.SetIdentityProvider(jwt => JwtIdentityAccessor.Resolve(jwt, configuration));

			var registration = Singleton<ServiceBusHandlerRegistration>.Instance;
			if (registration == null || registration.Assemblies.Count == 0)
			{
				throw new InvalidOperationException("No service bus handler assemblies registered.");
			}

			config.RegisterHandlers(registration.Assemblies.ToArray());
		});

		var name = configuration.GetValue<string>("ServiceBus:Provider")?.ToLower();
		switch (name)
		{
			case null:
			case "":
			case ServiceBusProvider.InMemory:
				bus.UseInMemory(opt => ConfigureInMemory(opt, configuration));
				break;
			case ServiceBusProvider.RabbitMq:
				bus.UseRabbitMq(opt => ConfigureRabbitMq(opt, configuration));
				break;
			default:
				throw new NotSupportedException($"The service bus provider '{name}' is not supported.");
		}

		return services;
	}

	///  <summary>
	/// 		Apply InMemory service bus specific settings from configuration.
	/// 		Reads the section identified by <see cref="ServiceBusProvider.ConfigurationSectionInMemory"/> and copies values into the provided options.
	///  </summary>
	///  <param name="options">The <see cref="InMemoryBusOptions"/> instance to configure.</param>
	///  <param name="configuration"></param>
	private static void ConfigureInMemory(InMemoryBusOptions options, IConfiguration configuration)
	{
		var section = configuration.GetSection(ServiceBusProvider.ConfigurationSectionInMemory).Get<InMemoryBusOptions>();
		if (section == null)
		{
			return;
		}

		options.MultipleSubscriberInstance = section.MultipleSubscriberInstance;
	}

	///  <summary>
	/// 		Apply RabbitMQ service bus specific settings from configuration.
	/// 		Reads the section identified by <see cref="ServiceBusProvider.ConfigurationSectionRabbitMq"/> and copies values into the provided options.
	///  </summary>
	///  <param name="options">The <see cref="RabbitMqMessageBusOptions"/> instance to configure.</param>
	///  <param name="configuration"></param>
	private static void ConfigureRabbitMq(RabbitMqMessageBusOptions options, IConfiguration configuration)
	{
		var section = configuration.GetSection(ServiceBusProvider.ConfigurationSectionRabbitMq).Get<RabbitMqMessageBusOptions>();
		if (section == null)
		{
			return;
		}

		options.Connection = section.Connection;
		options.ExchangeName = section.ExchangeName;
		options.ExchangeType = section.ExchangeType;
		options.QueueName = section.QueueName;
		options.TopicName = section.TopicName;
	}
}
