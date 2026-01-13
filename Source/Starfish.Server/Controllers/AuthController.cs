using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nerosoft.Starfish.Facade.Interfaces;
using Nerosoft.Starfish.Facade.Transit;

namespace Nerosoft.Starfish.Server.Controllers;

/// <summary>
/// The controller for identity operations.
/// </summary>
/// <param name="service"></param>
/// <param name="configuration"></param>
[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthApplicationService service, IConfiguration configuration) : ControllerBase
{
	/// <summary>
	/// Grant token
	/// </summary>
	/// <param name="request">The information to auth.</param>
	/// <returns></returns>
	[AllowAnonymous]
	[HttpPost("token/grant")]
	[Produces(typeof(TokenGrantResponseDto))]
	public async Task<IActionResult> GrantTokenAsync([FromBody] TokenGrantRequestDto request)
	{
		var result = await service.GrantAsync(request, HttpContext.RequestAborted);
		return Ok(result);
	}
}