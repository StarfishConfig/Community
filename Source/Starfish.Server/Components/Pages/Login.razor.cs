using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using Nerosoft.Starfish.Facade.Transit;


namespace Nerosoft.Starfish.Server.Components.Pages;

public partial class Login : ComponentBase
{
	[CascadingParameter]
	private HttpContext HttpContext { get; set; }

	private EditContext _editContext;

	[SupplyParameterFromQuery]
	private string ReturnUrl { get; set; } // => HttpContext?.Request.Query["returnUrl"] ?? "/";

	[SupplyParameterFromForm]
	private LoginModel Model { get; set; }
	
	private bool Loading { get; set; } = false;

	/// <summary>
	/// Initializes the component.
	/// </summary>
	protected override async Task OnInitializedAsync()
	{
		Model ??= new LoginModel();
		_editContext = new EditContext(Model);
		await base.OnInitializedAsync();
	}

	private async Task OnSubmitAsync(EditContext context)
	{
		var dto = new TokenGrantRequestDto
		{
			Username = Model.Username, Password = Model.Password, GrantType = "password"
		};
		if (HttpContext == null)
		{
			var result = await Service.GrantAsync(dto);
			await LocalStorage.SetAsync(IdentityConstants.BearerScheme, result);
			Navigation.NavigateTo(ReturnUrl);
		}
		else
		{
			var result = await Service.SignInAsync(dto);
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, result);
			HttpContext.Response.Redirect(ReturnUrl);
		}
	}

	private class LoginModel
	{
		[Required]
		public string Username { get; set; } = string.Empty;

		[Required]
		public string Password { get; set; } = string.Empty;
	}
}