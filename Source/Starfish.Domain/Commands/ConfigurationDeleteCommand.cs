using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Domain.Commands;

/// <summary>
/// Represents a command to delete an existing configuration.
/// </summary>
/// <remarks>
/// Carries the identifier of the configuration to be removed.
/// Validation, authorization, and the actual deletion are handled by the application layer.
/// </remarks>
public class ConfigurationDeleteCommand : Command
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ConfigurationDeleteCommand"/> class with the specified identifier.
	/// </summary>
	/// <param name="id">The identifier of the configuration to delete.</param>
	public ConfigurationDeleteCommand(long id)
	{
		Id = id;
	}

	/// <summary>
	/// Gets the identifier of the configuration to be deleted.
	/// </summary>
	public long Id { get; }
}