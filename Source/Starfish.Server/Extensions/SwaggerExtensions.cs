using Microsoft.OpenApi;

namespace Nerosoft.Starfish.Server;

internal static class SwaggerExtensions
{
	public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
	{
		services.AddOpenApi(options =>
		{
			options.AddDocumentTransformer((doc, _, _) =>
			{
				doc.Info.Title = "Starfish Server Api";
				doc.Info.Version = "v1";
				doc.Info.License = new OpenApiLicense
				{
					Name = "© 2020 Nerosoft. All Rights Reserved."
				};
				return Task.CompletedTask;
			});
		});
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen(gen =>
		{
			gen.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				Name = "Authorization",
				Type = SecuritySchemeType.Http,
				Scheme = "Bearer",
				BearerFormat = "JWT",
				In = ParameterLocation.Header,
				Description =
					"""
					JWT Authorization header using the Bearer scheme. 
					Enter 'Bearer' [space] and then your token in the text input below.
					Example: "Bearer 12345abcdef"
					"""
			});
			gen.AddSecurityDefinition("X-API-Key", new OpenApiSecurityScheme
			{
				Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
				Name = "Authorization",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.ApiKey,
				Scheme = "Bearer"
			});
			gen.AddSecurityRequirement(document => new OpenApiSecurityRequirement
			{
				[new OpenApiSecuritySchemeReference("Bearer", document)] = [],
				[new OpenApiSecuritySchemeReference("X-API-Key", document)] = []
			});

			gen.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
			foreach (var file in Directory.GetFiles(AppContext.BaseDirectory, "*.xml"))
			{
				gen.IncludeXmlComments(file);
			}

			gen.DocInclusionPredicate((docName, description) => true);
		});
		return services;
	}

	public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
	{
		// ReSharper disable once UnusedLambdaParameter
		app.UseSwagger(options =>
		{
			options.OpenApiVersion = OpenApiSpecVersion.OpenApi3_0;
			//option.SerializeAsV2 = true;
		});
		app.UseSwaggerUI(option =>
		{
			option.CacheLifetime = TimeSpan.FromMinutes(1);
			option.DocumentTitle = "Starfish Webapi Documentation";
			option.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
			// option.RoutePrefix = string.Empty;
			// option.RoutePrefix = string.Empty;
		});
		return app;
	}
}