namespace Nerosoft.Starfish.Facade;

internal static class MessageBusProvider
{
	public const string InMemory = nameof(InMemory);
	public const string RabbitMq = nameof(RabbitMq);

	public const string ConfigurationSectionRabbitMq = "Euonia:Bus:RabbitMq";
	public const string ConfigurationSectionInMemory = "Euonia:Bus:InMemory";
}