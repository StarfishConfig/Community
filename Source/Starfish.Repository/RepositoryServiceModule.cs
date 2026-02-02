using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nerosoft.Euonia.Caching;
using Nerosoft.Euonia.Caching.Memory;
using Nerosoft.Euonia.Caching.Redis;
using Nerosoft.Euonia.Caching.Runtime;
using Nerosoft.Euonia.Modularity;
using Nerosoft.Euonia.Repository;
using Nerosoft.Euonia.Repository.EfCore;

namespace Nerosoft.Starfish.Repository;

/// <summary>
/// Provides repository-level registrations and behavior for the bounded context.
/// </summary>
/// <remarks>
/// <para>
/// This module is responsible for registering repository implementations, repository contexts,
/// mappings and any repository-related services required by the features within the
/// application's modular dependency injection system.
/// </para>
/// <para>
/// The module derives from <see cref="ModuleContextBase"/> and is intended to be discovered
/// and initialized by the application's modularity/bootstrapper during startup. Concrete
/// repository types (for example, implementations of repository interfaces or DbContext types)
/// should be registered here so they become available to other modules and application services.
/// </para>
/// </remarks>
/// <example>
/// To ensure repository services are available at runtime, include this module in the application's
/// module registration sequence or bootstrapper. For example:
/// <code>
/// // Pseudo-code illustrating module registration
/// [DependsOn(typeof(RepositoryServiceModule))]
/// public class ApplicationModule : ModuleContextBase
/// {
///     // Module implementation
/// }
/// </code>
/// </example>
/// <seealso cref="ModuleContextBase"/>
[DependsOn(typeof(RepositoryModule))]
public class RepositoryServiceModule : ModuleContextBase
{
	/// <inheritdoc />
	public override void AheadConfigureServices(ServiceConfigurationContext context)
	{
		Configure<RedisCacheOptions>(Configuration.GetSection("Euonia:Caching:Redis"));
		Configure<MemoryCacheOptions>(Configuration.GetSection("Euonia:Caching:Memory"));
		Configure<RuntimeCacheOptions>(Configuration.GetSection("Euonia:Caching:Runtime"));
	}

	/// <inheritdoc />
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		context.Services.AddKeyedSingleton<ConnectionConfigurator>("inmemory", (builder, connectionString) => builder.UseInMemoryDatabase(connectionString));
		context.Services.AddKeyedSingleton<ConnectionConfigurator>("sqlite", (builder, connectionString) => builder.UseSqlite(connectionString));
		context.Services.AddKeyedSingleton<ConnectionConfigurator>("mssql", (builder, connectionString) => builder.UseSqlServer(connectionString));
		context.Services.AddKeyedSingleton<ConnectionConfigurator>("pgsql", (builder, connectionString) => builder.UseNpgsql(connectionString));
		context.Services.AddKeyedSingleton<ConnectionConfigurator>("mysql", (builder, connectionString) => builder.UseMySQL(connectionString, mySqlOptions =>
		{
			mySqlOptions.EnableRetryOnFailure();
		}));

		context.Services
		       .AddDataContextFactory<IdentityDataContext>()
		       .AddDataContextFactory<SystemDataContext>()
		       .AddDataContextFactory<ConfigsDataContext>();

		context.Services.AddKeyedSingleton<ICacheService, RedisCacheService>("Redis");
		context.Services.AddKeyedSingleton<ICacheService, MemoryCacheService>("Memory");
		context.Services.AddKeyedSingleton<ICacheService, RuntimeCacheService>("Runtime");
		context.Services.AddTransient<ICacheService, HybridCacheService>();
	}
}