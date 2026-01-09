using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.FluentUI.AspNetCore.Components;
using Nerosoft.Euonia.Hosting;
using Nerosoft.Euonia.Modularity;
using Nerosoft.Starfish.Facade;
using Nerosoft.Starfish.Server.Components;
using Serilog;

namespace Nerosoft.Starfish.Server;

[DependsOn(typeof(HostingModule), typeof(FacadeServiceModule))]
internal class ServerStartupModule : ModuleContextBase
{
	/// <summary>
	/// Add services to the container.
	/// </summary>
	/// <param name="context"></param>
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		context.Services.AddFluentUIComponents();
		context.Services.AddControllers();
		context.Services.AddHealthChecks();
		context.Services.AddSwaggerDocumentation();
		context.Services.AddCors(options =>
		{
			options.AddDefaultPolicy(policy =>
			{
				policy.WithOrigins(Configuration.GetSection("CorsOrigins").Get<string[]>());
				policy.AllowAnyHeader();
				policy.AllowAnyMethod();
				//builder.AllowCredentials();
			});
		});
		context.Services.AddGrpc(options =>
		{
			options.EnableDetailedErrors = true;
			options.MaxReceiveMessageSize = null;
			options.MaxSendMessageSize = null;
			options.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.SmallestSize;
			options.ResponseCompressionAlgorithm = "gzip";
		});

		context.Services.AddRazorComponents()
		       .AddInteractiveServerComponents()
		       .AddCircuitOptions(options =>
		       {
			       options.DetailedErrors = true;
		       });
	}

	public override void OnApplicationInitialization(ApplicationInitializationContext context)
	{
		var app = context.GetApplicationBuilder();
		app.ServerFeatures.Get<IServerAddressesFeature>();

		app.UseSerilogRequestLogging();
		app.UseForwardedHeaders();
		app.UseHttpsRedirection();

		app.UseAuthentication();
		app.UseRouting()
		   .UseCors(config =>
		   {
			   config.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
		   });
		app.UseAuthorization();
		app.UseDefaultRequestContextAccessor();
		app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
		app.UseAntiforgery();
		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllers();
			endpoints.MapHealthChecks("/health");
			//endpoints.MapGrpcService<StarfishGrpcService>();
			endpoints.MapStaticAssets();
			endpoints.MapRazorComponents<App>()
			         .AddInteractiveServerRenderMode();
		});
		
		app.UseExceptionHandler(new ExceptionHandlerOptions
		{
			AllowStatusCode404Response = true,
			StatusCodeSelector = exception => (int)exception.GetStatusCode(),
			ExceptionHandler = async httpContext =>
			{
				httpContext.Response.ContentType = "application/json";
				var exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
				var response = new
				{
					statusCode = httpContext.Response.StatusCode,
					error = exception?.GetErrorMessage(),
					details = exception?.GetErrorDetails()
				};
				await httpContext.Response.WriteAsJsonAsync(response);
			}
		});
	}
}