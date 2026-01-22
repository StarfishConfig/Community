using Nerosoft.Euonia.Application;
using Nerosoft.Starfish.Transit;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Facade.Interfaces;

/// <summary>
/// Application service contract for template-related operations.
/// Provides methods for retrieving, searching, creating, updating, and deleting templates.
/// </summary>
public interface ITemplateApplicationService : IApplicationService
{
    /// <summary>
    /// Retrieves detailed information for a specific template by its identifier.
    /// </summary>
    /// <param name="id">Unique identifier of the template.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A <see cref="TemplateDetailDto"/> containing the template's details.</returns>
    Task<TemplateDetailDto> GetAsync(long id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches templates by type and keyword with pagination.
    /// </summary>
    /// <param name="type">The type of the template to filter by.</param>
    /// <param name="keyword">Search term to match against template properties (name, description, etc.).</param>
    /// <param name="skip">Number of items to skip for pagination.</param>
    /// <param name="size">Maximum number of items to return.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A list of <see cref="TemplateListDto"/> matching the search criteria.</returns>
    Task<List<TemplateListDto>> SearchAsync(TemplateType type, string keyword, int skip, int size, CancellationToken cancellationToken = default);

    /// <summary>
    /// Counts templates that match the given type and keyword filter.
    /// </summary>
    /// <param name="type">The type of the template to filter by.</param>
    /// <param name="keyword">Search term to match against template properties.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The total number of templates matching the criteria.</returns>
    Task<int> CountAsync(TemplateType type, string keyword, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new template using the provided data.
    /// </summary>
    /// <param name="dto">Template data used to create the new template.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The unique identifier of the newly created template.</returns>
    Task<long> CreateAsync(TemplateEditDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing template identified by <paramref name="id"/> with the provided data.
    /// </summary>
    /// <param name="id">Identifier of the template to update.</param>
    /// <param name="dto">Updated template data.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    Task UpdateAsync(long id, TemplateEditDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a template identified by its unique identifier.
    /// </summary>
    /// <param name="id">Identifier of the template to delete.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}