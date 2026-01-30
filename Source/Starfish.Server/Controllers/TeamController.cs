using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nerosoft.Starfish.Facade.Interfaces;
using Nerosoft.Starfish.Infrastructure;
using Nerosoft.Starfish.Transit;

namespace Nerosoft.Starfish.Server.Controllers;

/// <summary>
/// API controller for managing teams.
/// All endpoints require an authenticated user.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TeamController(ITeamApplicationService service) : ControllerBase
{
	/// <summary>
	/// Application service that contains business logic for team operations.
	/// Provided via primary constructor injection.
	/// </summary>
	private readonly ITeamApplicationService _service = service;

	/// <summary>
	/// Gets a team by its unique identifier.
	/// </summary>
	/// <param name="id">The unique identifier of the team to retrieve.</param>
	/// <returns>
	/// An <see cref="IActionResult"/>:
	/// - <c>200 OK</c> with a <see cref="TeamDetailDto"/> when the team is found.
	/// - Appropriate error status (e.g. 404) if not found or unauthorized.
	/// </returns>
	[HttpGet("{id:long}")]
	public async Task<IActionResult> GetAsync([FromRoute] long id)
	{
		var result = await _service.GetAsync(id, HttpContext.RequestAborted);
		return Ok(result);
	}

	/// <summary>
	/// Searches for teams based on the specified criteria.
	/// </summary>
	/// <param name="keyword">Optional search keyword to filter teams by name, description, etc.</param>
	/// <param name="role">
	/// Role filter to indicate the relation of the current user to the teams:
	/// e.g. Owner, Ordinary. Use a value to restrict results to teams where the current user has that role.
	/// </param>
	/// <param name="skip">Number of items to skip for paging. Defaults to 0.</param>
	/// <param name="size">Page size for results. Defaults to 20.</param>
	/// <returns>
	/// An <see cref="IActionResult"/>:
	/// - <c>200 OK</c> with a list of <see cref="TeamListDto"/> matching the search criteria.
	/// </returns>
	[HttpGet("search")]
	public async Task<IActionResult> SearchAsync([FromQuery] string keyword, [FromQuery] TeamMemberRole role, [FromQuery] int skip = 0, [FromQuery] int size = 20)
	{
		var result = await _service.SearchAsync(keyword, role, skip, size, HttpContext.RequestAborted);
		return Ok(result);
	}

	/// <summary>
	/// Counts the number of teams matching the specified criteria.
	/// </summary>
	/// <param name="keyword">Optional search keyword to filter teams.</param>
	/// <param name="role">
	/// Role filter to indicate the relation of the current user to the teams:
	/// e.g. Owner, Ordinary. Use a value to restrict counting to teams where the current user has that role.
	/// </param>
	/// <returns>
	/// An <see cref="IActionResult"/>:
	/// - <c>200 OK</c> with the integer count of matching teams.
	/// The response header "X-Total-Count" is also set to the count value.
	/// </returns>
	[HttpHead("count")]
	public async Task<IActionResult> CountAsync([FromQuery] string keyword, [FromQuery] TeamMemberRole role)
	{
		var result = await _service.CountAsync(keyword, role, HttpContext.RequestAborted);
		HttpContext.Response.Headers.Append("X-Total-Count", result.ToString());
		return Ok(result);
	}

	/// <summary>
	/// Creates a new team.
	/// </summary>
	/// <param name="data">The team edit DTO containing details required for creation.</param>
	/// <returns>
	/// An <see cref="IActionResult"/>:
	/// - <c>201 Created</c> on successful creation.
	/// </returns>
	[HttpPost]
	public async Task<IActionResult> CreateAsync([FromBody] TeamEditDto data)
	{
		await _service.CreateAsync(data, HttpContext.RequestAborted);
		return Created();
	}

	/// <summary>
	/// Updates an existing team.
	/// </summary>
	/// <param name="id">The unique identifier of the team to update.</param>
	/// <param name="data">The team edit DTO containing updated values.</param>
	/// <returns>
	/// An <see cref="IActionResult"/>:
	/// - <c>204 No Content</c> when the update succeeds.
	/// - Appropriate error status (e.g. 404) if the team does not exist or unauthorized.
	/// </returns>
	[HttpPut("{id:long}")]
	public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] TeamEditDto data)
	{
		await _service.UpdateAsync(id, data, HttpContext.RequestAborted);
		return NoContent();
	}

	/// <summary>
	/// Deletes a team by its unique identifier.
	/// </summary>
	/// <param name="id">The unique identifier of the team to delete.</param>
	/// <returns>
	/// An <see cref="IActionResult"/>:
	/// - <c>204 No Content</c> when deletion succeeds.
	/// - Appropriate error status (e.g. 404) if the team does not exist or unauthorized.
	/// </returns>
	[HttpDelete("{id:long}")]
	public async Task<IActionResult> DeleteAsync([FromRoute] long id)
	{
		await _service.DeleteAsync(id, HttpContext.RequestAborted);
		return NoContent();
	}

	/// <summary>
	/// Transfers ownership of a team to another user.
	/// </summary>
	/// <param name="id">The unique identifier of the team to transfer.</param>
	/// <param name="data">Transfer parameters including the identifier of the target user and options such as whether the current owner leaves.</param>
	/// <returns>
	/// An <see cref="IActionResult"/>:
	/// - <c>204 No Content</c> when transfer succeeds.
	/// - Appropriate error status for invalid input or unauthorized actions.
	/// </returns>
	[HttpPost("{id:long}/transfer")]
	public async Task<IActionResult> TransferAsync([FromRoute] long id, [FromBody] TeamTransferDto data)
	{
		await _service.TransferAsync(id, data, HttpContext.RequestAborted);
		return NoContent();
	}

	/// <summary>
	/// Gets the list of members in a team.
	/// </summary>
	/// <param name="teamId">The unique identifier of the team whose members are requested.</param>
	/// <param name="keyword">Optional keyword to filter members by name, email, or other attributes.</param>
	/// <param name="skip">Number of items to skip for paging. Defaults to 0.</param>
	/// <param name="size">Page size for results. Defaults to 20.</param>
	/// <returns>
	/// An <see cref="IActionResult"/>:
	/// - <c>200 OK</c> with a list of <see cref="TeamMemberDto"/> for the requested page.
	/// </returns>
	[HttpGet("{teamId:long}/members/list")]
	public async Task<IActionResult> GetMemberListAsync([FromRoute] long teamId, [FromQuery] string keyword, [FromQuery] int skip = 0, [FromQuery] int size = 20)
	{
		var result = await _service.GetMemberListAsync(teamId, keyword, skip, size, HttpContext.RequestAborted);
		return Ok(result);
	}

	/// <summary>
	/// Gets the count of members in a team matching the optional keyword.
	/// </summary>
	/// <param name="teamId">The unique identifier of the team.</param>
	/// <param name="keyword">Optional keyword to filter members by name, email, etc.</param>
	/// <returns>
	/// An <see cref="IActionResult"/>:
	/// - <c>200 OK</c> with the integer count of matching members.
	/// </returns>
	[HttpGet("{teamId:long}/members/count")]
	public async Task<IActionResult> GetMemberCountAsync([FromRoute] long teamId, [FromQuery] string keyword)
	{
		var result = await _service.GetMemberCountAsync(teamId, keyword, HttpContext.RequestAborted);
		return Ok(result);
	}

	/// <summary>
	/// Appends members to the specified team.
	/// </summary>
	/// <param name="teamId">The unique identifier of the team to which users will be added.</param>
	/// <param name="userIds">List of user ids to add to the team.</param>
	/// <returns>
	/// An <see cref="IActionResult"/>:
	/// - <c>204 No Content</c> when members are appended successfully.
	/// - Appropriate error status for invalid users or unauthorized operations.
	/// </returns>
	[HttpPost("{teamId:long}/members")]
	public async Task<IActionResult> AppendMemberAsync([FromRoute] long teamId, [FromBody] IList<string> userIds)
	{
		await _service.AppendMemberAsync(teamId, userIds, HttpContext.RequestAborted);
		return NoContent();
	}

	/// <summary>
	/// Removes members from the specified team.
	/// </summary>
	/// <param name="teamId">The unique identifier of the team from which users will be removed.</param>
	/// <param name="userIds">List of user ids to remove from the team.</param>
	/// <returns>
	/// An <see cref="IActionResult"/>:
	/// - <c>204 No Content</c> when members are removed successfully.
	/// - Appropriate error status for invalid users or unauthorized operations.
	/// </returns>
	[HttpDelete("{teamId:long}/members")]
	public async Task<IActionResult> RemoveMemberAsync([FromRoute] long teamId, [FromBody] IList<string> userIds)
	{
		await _service.RemoveMemberAsync(teamId, userIds, HttpContext.RequestAborted);
		return NoContent();
	}
}