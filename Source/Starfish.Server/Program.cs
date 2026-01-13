using Nerosoft.Starfish.Server;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration) =>
{
	configuration.ReadFrom.Configuration(context.Configuration)
				 .Enrich.FromLogContext()
				 .Enrich.WithProperty("ApplicationName", context.HostingEnvironment.ApplicationName)
				 .Enrich.WithProperty("Environment", context.HostingEnvironment);

	configuration.WriteTo.Logger(config => config.Filter.ByIncludingOnly(@event => @event.Level == LogEventLevel.Debug)
												 .WriteTo.File("Logs/Debug/logs.log", rollingInterval: RollingInterval.Day))
				 .WriteTo.Logger(config => config.Filter.ByIncludingOnly(@event => @event.Level == LogEventLevel.Warning)
												 .WriteTo.File("Logs/Warning/logs.log", rollingInterval: RollingInterval.Day))
				 .WriteTo.Logger(config => config.Filter.ByIncludingOnly(@event => @event.Level == LogEventLevel.Error)
												 .WriteTo.File("Logs/Error/logs.log", rollingInterval: RollingInterval.Day))
				 .WriteTo.Logger(config => config.Filter.ByIncludingOnly(@event => @event.Level == LogEventLevel.Information)
												 .WriteTo.File("Logs/Info/logs.log", rollingInterval: RollingInterval.Day))
				 .WriteTo.Logger(config => config.Filter.ByIncludingOnly(@event => @event.Level == LogEventLevel.Fatal)
												 .WriteTo.File("Logs/Fatal/logs.log", rollingInterval: RollingInterval.Day));
});
builder.Host.UseDefaultServiceProvider((_, options) =>
{
	options.ValidateScopes = false;
});

builder.Services.AddModularityApplication<ServerStartupModule>(builder.Configuration);

// builder.Services.AddAuthentication(options =>
//        {
// 	       options.DefaultScheme = IdentityConstants.ApplicationScheme;
// 	       options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
// 		   options.DefaultAuthenticateScheme = "Cookies";
// 		   //options.DefaultSignInScheme = "Cookies";
// 		   options.DefaultChallengeScheme = "Cookies";
// 	   })
// 	   .AddCookie("Cookies", options =>
// 	   {
// 		   options.LoginPath = "/login";
// 		   options.LogoutPath = "/logout";
// 	   });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
else
{
	app.MapOpenApi();
	app.UseSwaggerDocumentation();
}

app.InitializeApplication();

await app.RunAsync();