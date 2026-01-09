using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Duende.IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nerosoft.Starfish.Facade.Interfaces;
using Nerosoft.Starfish.Facade.Transit;
using Nerosoft.Starfish.Shared;
using Nerosoft.Starfish.Toolkit;

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
	private const string JWT_AUTH_SECTION = "JwtAuthenticationOptions";

	/// <summary>
	/// Grant token
	/// </summary>
	/// <param name="request">The information to auth.</param>
	/// <returns></returns>
	[AllowAnonymous]
	[HttpPost("token/grant")]
	[Produces(typeof(AuthResponseDto))]
	public async Task<IActionResult> GrantTokenAsync([FromBody] AuthRequestDto request)
	{
		var principal = await service.GrantAsync(JwtConstants.TokenType, request, HttpContext.RequestAborted);
		var result = GenerateAccessToken(principal);
		return Ok(result);
	}

	/// <summary>
	/// Generate access token
	/// </summary>
	/// <param name="principal"></param>
	/// <returns></returns>
	private AuthResponseDto GenerateAccessToken(ClaimsPrincipal principal)
	{
		//var roles = user.Roles?.Select(r => r.Name);

		//var jti = ObjectId.NewGuid(GuidType.SequentialAsString).ToString("N");

		var issueTime = DateTime.UtcNow;
		var expiresAt = issueTime.AddDays(1);

		var builder = TokenGenerator.From(principal)
		                            .WithSigningKey(configuration.GetValue<string>($"{JWT_AUTH_SECTION}:SigningKey"))
		                            .WithIssuer(configuration.GetValue<string>($"{JWT_AUTH_SECTION}:Issuer:0"))
		                            .IssuedAt(issueTime);

		var accessToken = builder.Build();

		var username = principal.FindFirstValue(JwtClaimTypes.Name);
		var userId = principal.FindFirstValue(JwtClaimTypes.Subject);

		return new AuthResponseDto
		{
			AccessToken = accessToken,
			RefreshToken = ObjectId.NewGuid(GuidType.SequentialAsString).ToString("N"),
			TokenType = TokenType.Bearer,
			Username = username,
			UserId = userId,
			IssueAt = new DateTimeOffset(issueTime).ToUnixTimeSeconds(),
			ExpiresIn = (long)(expiresAt - issueTime).TotalSeconds
		};
	}
}