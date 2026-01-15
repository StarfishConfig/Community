using Microsoft.AspNetCore.Mvc;
using Nerosoft.Starfish.Facade.Interfaces;
using Nerosoft.Starfish.Facade.Transit;

namespace Nerosoft.Starfish.Server.Controllers;

/// <summary>
/// Controller for managing message templates.
/// </summary>
/// <param name="service"></param>
[Route("api/message/template")]
[ApiController]
public class MessageTemplateController(IMessageTemplateAppService service) : ControllerBase
{
	/// <summary>
	/// Gets a message template by its unique identifier.
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	[HttpGet("{id:long}")]
	public async Task<IActionResult> GetAsync([FromRoute] long id)
	{
		var result = await service.GetAsync(id, HttpContext.RequestAborted);
		return Ok(result);
	}

	/// <summary>
	/// Queries message templates based on specified criteria with pagination.
	/// </summary>
	/// <param name="criteria"></param>
	/// <param name="skip"></param>
	/// <param name="size"></param>
	/// <returns></returns>
	[HttpGet("query")]
	public async Task<IActionResult> QueryAsync([FromQuery] MessageTemplateCriteria criteria, [FromQuery] int skip = 0, [FromQuery] int size = 20)
	{
		ArgumentNullException.ThrowIfNull(criteria);
		var result = await service.QueryAsync(criteria, skip, size, HttpContext.RequestAborted);
		return Ok(result);
	}

	/// <summary>
	/// Counts the number of message templates that match the specified criteria.
	/// </summary>
	/// <param name="criteria"></param>
	/// <returns></returns>
	[HttpGet("count")]
	public async Task<IActionResult> CountAsync([FromQuery] MessageTemplateCriteria criteria)
	{
		var result = await service.CountAsync(criteria, HttpContext.RequestAborted);
		return Ok(result);
	}

	/// <summary>
	/// Creates a new message template.
	/// </summary>
	/// <param name="dto"></param>
	/// <returns></returns>
	[HttpPost]
	public async Task<IActionResult> CreateAsync([FromBody] MessageTemplateEditDto dto)
	{
		ArgumentNullException.ThrowIfNull(dto);
		var id = await service.CreateAsync(dto, HttpContext.RequestAborted);
		return Created($"api/message/template/{id}", id);
	}

	/// <summary>
	/// Updates an existing message template.
	/// </summary>
	/// <param name="id"></param>
	/// <param name="dto"></param>
	/// <returns></returns>
	[HttpPut("{id:long}")]
	public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] MessageTemplateEditDto dto)
	{
		ArgumentNullException.ThrowIfNull(dto);
		await service.UpdateAsync(id, dto, HttpContext.RequestAborted);
		return NoContent();
	}

	/// <summary>
	/// Deletes a message template by its unique identifier.
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	[HttpDelete("{id:long}")]
	public async Task<IActionResult> DeleteAsync([FromRoute] long id)
	{
		await service.DeleteAsync(id, HttpContext.RequestAborted);
		return NoContent();
	}
}