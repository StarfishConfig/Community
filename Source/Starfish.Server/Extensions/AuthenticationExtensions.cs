using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Duende.IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Nerosoft.Starfish.Server;

internal static class AuthenticationExtensions
{
	private const string AUTHENTICATION_SECTION = "Authentication";
	private const string AUTHENTICATION_SECTION_USE_POLICY = AUTHENTICATION_SECTION + ":UsePolicy";
	private const string AUTHENTICATION_SECTION_SCHEME = AUTHENTICATION_SECTION + ":Scheme";
	private const string AUTHENTICATION_SECTION_BEARER = AUTHENTICATION_SECTION + ":Bearer";
	private const string AUTHENTICATION_SECTION_COOKIE = AUTHENTICATION_SECTION + ":Cookie";

	public static IServiceCollection AddAuthenticationHandlers(this IServiceCollection services, IConfiguration configuration)
	{
		JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

		var usePolicy = configuration.GetValue(AUTHENTICATION_SECTION_USE_POLICY, false);

		if (usePolicy)
		{
			services.AddAuthorizationBuilder()
			        .AddPolicy(JwtBearerDefaults.AuthenticationScheme, policy =>
			        {
				        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
				        policy.RequireClaim(JwtClaimTypes.Subject);
				        policy.RequireClaim(JwtClaimTypes.Name);
			        })
			        .AddPolicy(CookieAuthenticationDefaults.AuthenticationScheme, policy =>
			        {
				        policy.AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme);
				        policy.RequireClaim(ClaimTypes.NameIdentifier);
				        policy.RequireClaim(ClaimTypes.Name);
			        });
		}

		services.AddAuthentication(options =>
		        {
			        var scheme = configuration.GetValue<string>(AUTHENTICATION_SECTION_SCHEME)?.Trim();
			        switch (configuration.GetValue<string>(AUTHENTICATION_SECTION_SCHEME)?.Trim())
			        {
				        case null or "":
					        throw new ConfigurationException("Authentication scheme is not configured.");
				        case JwtBearerDefaults.AuthenticationScheme:
					        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
					        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					        options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
					        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
					        break;
				        case CookieAuthenticationDefaults.AuthenticationScheme:
					        options.DefaultScheme = IdentityConstants.ApplicationScheme;
					        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
					        //options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
					        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
					        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
					        break;
				        default: // custom scheme defined in .AddPolicyScheme() below
					        options.DefaultScheme = scheme;
					        options.DefaultAuthenticateScheme = scheme;
					        break;
			        }
		        })
		        .AddCookie(options =>
		        {
			        options.LoginPath = configuration[$"{AUTHENTICATION_SECTION_COOKIE}:LoginPath"];
			        options.ExpireTimeSpan = TimeSpan.FromDays(1);
			        options.LogoutPath = configuration[$"{AUTHENTICATION_SECTION_COOKIE}:LogoutPath"];
			        options.AccessDeniedPath = configuration[$"{AUTHENTICATION_SECTION_COOKIE}:AccessDeniedPath"];
			        options.Cookie.SameSite = SameSiteMode.Strict;
			        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
			        options.Cookie.IsEssential = true;
			        options.SlidingExpiration = configuration.GetValue($"{AUTHENTICATION_SECTION_COOKIE}:SlidingExpiration", true);
			        options.Events = new CookieAuthenticationEvents()
			        {
				        OnRedirectToLogin = async context =>
				        {
					        // If the request is for an API endpoint, return 401 Unauthorized instead of redirecting
					        if (context.Request.Path.Value?.StartsWith("/api") == true)
					        {
						        context.Response.Clear();
						        context.Response.StatusCode = 401;
					        }
					        else
					        {
						        context.Response.Redirect(context.RedirectUri);
					        }

					        await Task.CompletedTask;
				        }
			        };
					//options.SessionStore
		        })
		        .AddJwtBearer(options =>
		        {
			        var bearerOptions = configuration.GetSection(AUTHENTICATION_SECTION_BEARER).Get<JwtAuthenticationOptions>();

			        options.Authority = bearerOptions.Authority;
			        options.RequireHttpsMetadata = bearerOptions.RequireHttpsMetadata;
			        options.Audience = bearerOptions.Audience;

			        options.Events = new JwtBearerEvents()
			        {
				        OnMessageReceived = context =>
				        {
					        var authorizationValue = context.Request.Headers.Authorization.ToString();

					        var token = Regex.Match(authorizationValue, @"^Bearer\s+(.*)").Groups[1].Value;

					        context.Token = token;
					        return Task.CompletedTask;
				        },
				        OnChallenge = context =>
				        {
					        System.Diagnostics.Debug.WriteLine(context.Error);
					        Console.WriteLine(context.ErrorDescription);
					        return Task.CompletedTask;
				        },
				        OnAuthenticationFailed = context =>
				        {
					        System.Diagnostics.Debug.WriteLine(context.Result?.Failure);
					        System.Diagnostics.Debug.WriteLine(context.Exception);
					        return Task.CompletedTask;
				        },
				        OnForbidden = context =>
				        {
					        System.Diagnostics.Debug.WriteLine(context.Result?.Failure);
					        return Task.CompletedTask;
				        },
				        OnTokenValidated = context =>
				        {
					        System.Diagnostics.Debug.WriteLine(context.Result?.Failure);
					        return Task.CompletedTask;
				        }
			        };

			        options.TokenValidationParameters = new TokenValidationParameters
			        {
				        NameClaimType = bearerOptions.NameClaimType,
				        RoleClaimType = bearerOptions.RoleClaimType,
				        ValidIssuers = bearerOptions.Issuer,
				        ValidateIssuer = bearerOptions.ValidateIssuer,
				        ValidateAudience = bearerOptions.ValidateAudience,
				        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(bearerOptions.SigningKey)),
				        // ValidIssuer = "localhost",
				        // ValidAudience = "your_audience",
				        ValidateIssuerSigningKey = true,
				        //IssuerSigningKey = new SymmetricSecurityKey("your_signing_key"u8.ToArray())
			        };
		        }) // this is the key piece!
		        .AddPolicyScheme("Default", "Authentication Scheme Policy", options =>
		        {
			        // runs on each request
			        options.ForwardDefaultSelector = context =>
			        {
				        // choose scheme based on request path
				        // use JWT Bearer for API endpoints, Cookie for others
				        return context.Request.Path.StartsWithSegments("/api") ? JwtBearerDefaults.AuthenticationScheme : CookieAuthenticationDefaults.AuthenticationScheme;
				        /*
				        // filter by auth type
				        string authorization = context.Request.Headers.Authorization;
				        // Returns Bearer if authorization starts with 'Bearer ', otherwise always check for cookie auth
				        return authorization?.StartsWith("Bearer ") == true ? JwtBearerDefaults.AuthenticationScheme : CookieAuthenticationDefaults.AuthenticationScheme;
				        */
			        };
			        //options.ForwardDefault = JwtBearerDefaults.AuthenticationScheme;
		        });
		return services;
	}
}