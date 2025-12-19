namespace Nerosoft.Starfish.Facade;

internal static class MessageBusProvider
{
	public const string InMemory = nameof(InMemory);
	public const string RabbitMq = nameof(RabbitMq);

	public const string ConfigurationSectionRabbitMq = "EuoniaBus:RabbitMq";
	public const string ConfigurationSectionInMemory = "EuoniaBus:InMemory";
}