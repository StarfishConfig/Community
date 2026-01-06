using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nerosoft.Euonia.Mapping;
using Nerosoft.Euonia.Modularity;
using Nerosoft.Euonia.Repository;
using Nerosoft.Euonia.Repository.EfCore;
using Nerosoft.Starfish.Domain;
using Nerosoft.Starfish.Persist.Mappers;

namespace Nerosoft.Starfish.Persist;

/// <summary>
/// Provides persistent-level registrations and behavior for the bounded context.
/// </summary>
/// <remarks>
/// <para>
/// This module is responsible for registering persistent implementations, persistence contexts,
/// mappings and any persistent-related services required by the features within the
/// application's modular dependency injection system.
/// </para>
/// <para>
/// The module derives from <see cref="ModuleContextBase"/> and is intended to be discovered
/// and initialized by the application's modularity/bootstrapper during startup. Concrete
/// persistent types (for example, implementations of persistent interfaces or DbContext types)
/// should be registered here so they become available to other modules and application services.
/// </para>
/// </remarks>
/// <example>
/// To ensure persistent services are available at runtime, include this module in the application's
/// module registration sequence or bootstrapper. For example:
/// <code>
/// // Pseudo-code illustrating module registration
/// var bootstrapper = new ApplicationBootstrapper();
/// bootstrapper.RegisterModule&lt;PersistentServiceModule&gt;();
/// bootstrapper.Initialize();
/// </code>
/// </example>
/// <seealso cref="ModuleContextBase"/>
[DependsOn(typeof(DomainServiceModule), typeof(RepositoryModule))]
public class PersistServiceModule : ModuleContextBase
{
	/// <inheritdoc />
	public override void AheadConfigureServices(ServiceConfigurationContext context)
	{
		Configure<AutomapperOptions>(options =>
		{
			options.AddProfile<UserMapperProfile>();
		});
	}

	/// <inheritdoc />
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		context.Services.AddKeyedSingleton<ConnectionConfigurator>("inmemory", (builder, connectionString) => builder.UseInMemoryDatabase(connectionString));
		context.Services.AddKeyedSingleton<ConnectionConfigurator>("sqlite", (builder, connectionString) => builder.UseSqlite(connectionString));
		context.Services.AddKeyedSingleton<ConnectionConfigurator>("mssql", (builder, connectionString) => builder.UseSqlServer(connectionString));
		context.Services.AddKeyedSingleton<ConnectionConfigurator>("pgsql", (builder, connectionString) => builder.UseNpgsql(connectionString));

		context.Services.AddDataContextFactory<IdentityDataContext>();
	}
}