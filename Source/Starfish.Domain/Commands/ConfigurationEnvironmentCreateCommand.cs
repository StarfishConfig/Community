using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Domain.Commands;

/// <summary>
/// Represents a command to create a new environment for a configuration.
/// </summary>
/// <remarks>
/// This command is used to add a new environment (e.g., Development, Staging, Production) 
/// to an existing configuration, allowing for environment-specific settings and access control.
/// </remarks>
public class ConfigurationEnvironmentCreateCommand : Command
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ConfigurationEnvironmentCreateCommand"/> class.
	/// </summary>
	/// <param name="configurationId">The unique identifier of the configuration to which the environment will be added.</param>
	public ConfigurationEnvironmentCreateCommand(long configurationId)
	{
		ConfigurationId = configurationId;
	}

	/// <summary>
	/// Gets the unique identifier of the configuration to which the environment will be added.
	/// </summary>
	public long ConfigurationId { get; }

	/// <summary>
	/// Gets or sets the name of the environment to be created.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the secret key used for authentication and authorization when accessing this environment.
	/// </summary>
	public string Secret { get; set; }

	/// <summary>
	/// Gets or sets the description of the environment, providing additional context or information about its purpose and usage.
	/// </summary>
	public string Description { get; set; }
}