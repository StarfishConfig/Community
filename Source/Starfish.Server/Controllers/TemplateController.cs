using Microsoft.AspNetCore.Mvc;
using Nerosoft.Starfish.Facade.Interfaces;
using Nerosoft.Starfish.Facade.Transit;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Server.Controllers;

/// <summary>
/// Controller for managing message templates.
/// </summary>
/// <param name="service"></param>
[Route("api/[controller]")]
[ApiController]
public class TemplateController(ITemplateApplicationService service) : ControllerBase
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
	/// Queries message templates based on the specified criteria.
	/// </summary>
	/// <param name="type"></param>
	/// <param name="keyword"></param>
	/// <param name="skip"></param>
	/// <param name="size"></param>
	/// <returns></returns>
	[HttpGet("search")]
	public async Task<IActionResult> SearchAsync([FromQuery] TemplateType type, [FromQuery] string keyword, [FromQuery] int skip = 0, [FromQuery] int size = 20)
	{
		var result = await service.SearchAsync(type, keyword, skip, size, HttpContext.RequestAborted);
		return Ok(result);
	}

	/// <summary>
	/// Counts the number of message templates matching the specified criteria.
	/// </summary>
	/// <param name="type"></param>
	/// <param name="keyword"></param>
	/// <returns></returns>
	[HttpGet("count")]
	public async Task<IActionResult> CountAsync([FromQuery] TemplateType type, [FromQuery] string keyword)
	{
		var result = await service.CountAsync(type, keyword, HttpContext.RequestAborted);
		return Ok(result);
	}

	/// <summary>
	/// Creates a new message template.
	/// </summary>
	/// <param name="dto"></param>
	/// <returns></returns>
	[HttpPost]
	public async Task<IActionResult> CreateAsync([FromBody] TemplateEditDto dto)
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
	public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] TemplateEditDto dto)
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