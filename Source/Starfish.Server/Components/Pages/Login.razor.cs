using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Build.Framework;
using Nerosoft.Starfish.Facade.Transit;

namespace Nerosoft.Starfish.Server.Components.Pages;

public partial class Login : ComponentBase
{
	[CascadingParameter]
	private HttpContext? HttpContext { get; set; }

	private EditContext editContext = default!;

	private string ReturnUrl => HttpContext?.Request.Query["returnUrl"] ?? "/";

	[SupplyParameterFromForm]
	private LoginModel Model { get; set; } = default;

	private bool Loading { get; set; } = false;

	protected override async Task OnInitializedAsync()
	{
		Model ??= new LoginModel();
		editContext = new EditContext(Model);
	}

	private async Task OnLoginAsync(MouseEventArgs args)
	{
		var result = await Service.GrantAsync("Cookies", new AuthRequestDto { Username = Model.Username, Password = Model.Password, GrantType = "username" });
		await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { })));
		HttpContext.Response.Redirect(ReturnUrl);
	}

	private async Task OnSubmitAsync(EditContext context)
	{
		var result = await Service.GrantAsync("Cookies", new AuthRequestDto { Username = Model.Username, Password = Model.Password, GrantType = "username" });
		await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { })));
		HttpContext.Response.Redirect(ReturnUrl);
	}

	private class LoginModel
	{
		[Required]
		public string Username { get; set; } = string.Empty;

		[Required]
		public string Password { get; set; } = string.Empty;
	}
}