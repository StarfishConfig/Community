namespace Nerosoft.Starfish.Facade;

internal static class ServiceBusProvider
{
	public const string InMemory = "inmemory";
	public const string RabbitMq = "rabbitmq";

	public const string ConfigurationSectionRabbitMq = "ServiceBus:RabbitMq";
	public const string ConfigurationSectionInMemory = "ServiceBus:InMemory";
}