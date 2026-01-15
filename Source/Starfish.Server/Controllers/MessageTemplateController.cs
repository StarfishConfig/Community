using Microsoft.AspNetCore.Mvc;
using Nerosoft.Starfish.Facade.Interfaces;
using Nerosoft.Starfish.Facade.Transit;

namespace Nerosoft.Starfish.Server.Controllers;

[Route("api/message/template")]
[ApiController]
public class MessageTemplateController(IMessageTemplateAppService service) : ControllerBase
{
	[HttpGet("{id}")]
	public async Task<IActionResult> GetAsync([FromRoute] string id)
	{
		var result = await service.GetAsync(id, HttpContext.RequestAborted);
		return Ok(result);
	}

	[HttpGet("query")]
	public async Task<IActionResult> QueryAsync([FromQuery] MessageTemplateCriteria criteria, [FromQuery] int skip = 0, [FromQuery] int size = 20)
	{
		ArgumentNullException.ThrowIfNull(criteria);
		var result = await service.QueryAsync(criteria, skip, size, HttpContext.RequestAborted);
		return Ok(result);
	}

	[HttpGet("count")]
	public async Task<IActionResult> CountAsync([FromQuery] MessageTemplateCriteria criteria)
	{
		var result = await service.CountAsync(criteria, HttpContext.RequestAborted);
		return Ok(result);
	}

	[HttpPost]
	public async Task<IActionResult> CreateAsync([FromBody] MessageTemplateEditDto dto)
	{
		ArgumentNullException.ThrowIfNull(dto);
		var id = await service.CreateAsync(dto, HttpContext.RequestAborted);
		return CreatedAtAction(nameof(GetAsync), new { id }, null);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateAsync([FromRoute] string id, [FromBody] MessageTemplateEditDto dto)
	{
		ArgumentNullException.ThrowIfNull(dto);
		await service.UpdateAsync(id, dto, HttpContext.RequestAborted);
		return NoContent();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteAsync([FromRoute] string id)
	{
		await service.DeleteAsync(id, HttpContext.RequestAborted);
		return NoContent();
	}
}
