using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Nerosoft.Starfish.Facade.Transit;

namespace Nerosoft.Starfish.Server;

internal class PersistentAuthenticationStateProvider : ServerAuthenticationStateProvider
{
	private readonly PersistentComponentState _state;
	private readonly PersistingComponentStateSubscription _subscription;
	private readonly ProtectedLocalStorage _storage;
	private readonly CookieContainer _cookieContainer = new();
	private Task<AuthenticationState> _authenticationStateTask;

	public PersistentAuthenticationStateProvider(PersistentComponentState state, ProtectedLocalStorage storage)
	{
		_state = state;
		_storage = storage;
		AuthenticationStateChanged += OnAuthenticationStateChanged;
		_subscription = _state.RegisterOnPersisting(OnPersistingAsync, RenderMode.InteractiveServer);
	}

	public override async Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		var token = await _storage.GetAsync<TokenGrantResponseDto>(IdentityConstants.BearerScheme);
		ClaimsPrincipal principal;
		if (token.Success && !string.IsNullOrEmpty(token.Value?.AccessToken))
		{
			// Rehydrate authentication state from stored token as needed
			var handler = new JwtSecurityTokenHandler();
			var jwtToken = handler.ReadJwtToken(token.Value.AccessToken);
			var identity = new ClaimsIdentity(jwtToken.Claims, JwtBearerDefaults.AuthenticationScheme);
			principal = new ClaimsPrincipal(identity);
		}
		else
		{
			principal = new ClaimsPrincipal(new ClaimsIdentity());
		}

		return new AuthenticationState(principal);
	}

	private async Task OnPersistingAsync()
	{
		if (_authenticationStateTask is null)
		{
			throw new UnreachableException($"Authentication state not set in {nameof(OnPersistingAsync)}().");
		}

		var authenticationState = await _authenticationStateTask;
		var principal = authenticationState.User;

		if (principal.Identity?.IsAuthenticated == true)
		{
			var token = await _storage.GetAsync<TokenGrantResponseDto>(IdentityConstants.BearerScheme);
			if (token.Success && !string.IsNullOrEmpty(token.Value?.AccessToken))
			{
				_state.PersistAsJson(IdentityConstants.BearerScheme, token.Value);
			}
		}
	}

	private void OnAuthenticationStateChanged(Task<AuthenticationState> task)
	{
		_authenticationStateTask = task;
	}
}