using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nerosoft.Starfish.Facade.Interfaces;
using Nerosoft.Starfish.Facade.Transit;

namespace Nerosoft.Starfish.Server.Controllers;

/// <summary>
/// Controller for managing teams.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TeamController(ITeamApplicationService service) : ControllerBase
{
	/// <summary>
	/// Application service that contains business logic for team operations.
	/// </summary>
	private readonly ITeamApplicationService _service = service;

	/// <summary>
	/// Gets a team by its unique identifier.
	/// </summary>
	/// <param name="id">The unique identifier of the team.</param>
	/// <returns>
	/// Returns an <see cref="IActionResult"/> containing the team DTO if found (HTTP 200),
	/// or an appropriate error response if not found.
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
	/// <param name="keyword">Optional search keyword to filter teams by name or description.</param>
	/// <param name="owned">Given a value to indicate if only include the teams which the current user owned.</param>
	/// <param name="skip">Number of items to skip for paging. Defaults to 0.</param>
	/// <param name="size">Page size for results. Defaults to 20.</param>
	/// <returns>
	/// Returns an <see cref="IActionResult"/> with a paged list of matching teams (HTTP 200).
	/// </returns>
	[HttpGet("search")]
	public async Task<IActionResult> SearchAsync([FromQuery] string keyword, [FromQuery] bool? owned, [FromQuery] int skip = 0, [FromQuery] int size = 20)
	{
		// owned=true: only teams owned by current user
		// owned=false: only teams where current user is a member but not owner
		// owned=null: all teams where current user is owner or member
		var result = await _service.SearchAsync(keyword, owned, skip, size, HttpContext.RequestAborted);
		return Ok(result);
	}

	/// <summary>
	/// Counts the number of teams matching the specified criteria.
	/// </summary>
	/// <param name="keyword">Optional search keyword to filter teams.</param>
	/// <param name="owned">Given a value to indicate if only include the teams which the current user owned.</param>
	/// <returns>
	/// Returns an <see cref="IActionResult"/> containing the count of matching teams (HTTP 200).
	/// </returns>
	[HttpHead("count")]
	public async Task<IActionResult> CountAsync([FromQuery] string keyword, [FromQuery] bool? owned)
	{
		var result = await _service.CountAsync(keyword, owned, HttpContext.RequestAborted);
		HttpContext.Response.Headers.Append("X-Total-Count", result.ToString());
		return Ok(result);
	}

	/// <summary>
	/// Creates a new team.
	/// </summary>
	/// <param name="data">The team data transfer object containing details for creation.</param>
	/// <returns>
	/// Returns HTTP 201 Created on success.
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
	/// Returns HTTP 204 No Content on success.
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
	/// Returns HTTP 204 No Content on success.
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
	/// <param name="data">Transfer parameters including target user id and whether the current user leaves.</param>
	/// <returns>
	/// Returns HTTP 204 No Content on success.
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
	/// <param name="teamId">The unique identifier of the team.</param>
	/// <param name="keyword">Optional keyword to filter members (e.g., name or email).</param>
	/// <param name="skip">Number of items to skip for paging. Defaults to 0.</param>
	/// <param name="size">Page size for results. Defaults to 20.</param>
	/// <returns>
	/// Returns an <see cref="IActionResult"/> with a paged list of team members (HTTP 200).
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
	/// <param name="keyword">Optional keyword to filter members.</param>
	/// <returns>
	/// Returns HTTP 204 No Content and sets the response header "X-Total-Count" to the member count.
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
	/// <param name="teamId">The unique identifier of the team.</param>
	/// <param name="userIds">List of user ids to add to the team.</param>
	/// <returns>
	/// Returns HTTP 204 No Content on success.
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
	/// <param name="teamId">The unique identifier of the team.</param>
	/// <param name="userIds">List of user ids to remove from the team.</param>
	/// <returns>
	/// Returns HTTP 204 No Content on success.
	/// </returns>
	[HttpDelete("{teamId:long}/members")]
	public async Task<IActionResult> RemoveMemberAsync([FromRoute] long teamId, [FromBody] IList<string> userIds)
	{
		await _service.RemoveMemberAsync(teamId, userIds, HttpContext.RequestAborted);
		return NoContent();
	}
}