using Nerosoft.Euonia.Application;
using Nerosoft.Starfish.Transit;

namespace Nerosoft.Starfish.Facade.Interfaces;

/// <summary>
/// Application service interface for managing configurations.
/// Implementations provide search, retrieval, creation, update and deletion operations scoped to a team.
/// </summary>
public interface IConfigurationApplicationService : IApplicationService
{
	/// <summary>
	/// Searches configurations by a keyword within the specified team scope and returns a paged list.
	/// </summary>
	/// <param name="keyword">Search term to match configuration properties (name, description, keys, etc.).</param>
	/// <param name="teamId">Identifier of the team to which configurations belong. Use 0 or a negative value if not scoped to a team (behavior depends on implementation).</param>
	/// <param name="skip">Number of items to skip for paging.</param>
	/// <param name="size">Maximum number of items to return.</param>
	/// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
	/// <returns>A <see cref="List{ConfigurationListDto}"/> containing matching configuration list entries.</returns>
	Task<List<ConfigurationListDto>> SearchAsync(string keyword, long teamId, int skip, int size, CancellationToken cancellationToken = default);

	/// <summary>
	/// Counts configurations that match the provided keyword within the specified team scope.
	/// </summary>
	/// <param name="keyword">Search term to match configuration properties.</param>
	/// <param name="teamId">Identifier of the team to which configurations belong.</param>
	/// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
	/// <returns>The total number of configurations matching the criteria.</returns>
	Task<int> CountAsync(string keyword, long teamId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves detailed information of a configuration by its identifier.
	/// </summary>
	/// <param name="id">The unique identifier of the configuration to retrieve.</param>
	/// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
	/// <returns>A <see cref="ConfigurationDetailDto"/> representing the configuration details.</returns>
	Task<ConfigurationDetailDto> GetAsync(long id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Creates a new configuration using the provided <paramref name="data"/>.
	/// </summary>
	/// <param name="data">Data required to create the configuration.</param>
	/// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
	/// <returns>A task that represents the asynchronous create operation.</returns>
	Task CreateAsync(ConfigurationCreateDto data, CancellationToken cancellationToken = default);

	/// <summary>
	/// Updates an existing configuration identified by <paramref name="id"/> with the provided <paramref name="data"/>.
	/// </summary>
	/// <param name="id">The primary key identifier of the configuration to be updated. Must be greater than zero and reference an existing configuration.</param>
	/// <param name="data">Updated configuration values to persist.</param>
	/// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
	/// <returns>A task that represents the asynchronous update operation.</returns>
	Task UpdateAsync(long id, ConfigurationUpdateDto data, CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes the configuration identified by <paramref name="id"/>.
	/// </summary>
	/// <param name="id">The unique identifier of the configuration to delete.</param>
	/// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
	/// <returns>A task that represents the asynchronous delete operation.</returns>
	Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}