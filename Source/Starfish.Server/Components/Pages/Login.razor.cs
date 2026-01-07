using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Nerosoft.Starfish.Facade.Transit;

namespace Nerosoft.Starfish.Server.Components.Pages;

public partial class Login : ComponentBase
{
	[CascadingParameter]
	private HttpContext? HttpContext { get; set; }

	private string ReturnUrl => HttpContext?.Request.Query["returnUrl"] ?? "/";

	private string Username { get; set; } = string.Empty;

	private string Password { get; set; } = string.Empty;

	private async Task OnLoginAsync(MouseEventArgs args)
	{
		var result = await Service.GrantAsync(new AuthRequestDto { Username = Username, Password = Password, Provider = "username" });
		await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { })));
		HttpContext.Response.Redirect(ReturnUrl);
	}
}