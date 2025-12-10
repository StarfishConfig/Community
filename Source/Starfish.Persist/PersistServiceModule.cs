using Microsoft.Extensions.DependencyInjection;
using Nerosoft.Euonia.Modularity;
using Nerosoft.Euonia.Repository;
using Nerosoft.Starfish.Domain;

namespace Nerosoft.Starfish.Persist;

[DependsOn(typeof(DomainServiceModule))]
internal class PersistServiceModule : ModuleContextBase
{
	public override void AheadConfigureServices(ServiceConfigurationContext context)
	{
		Configure<UnitOfWorkOptions>(options =>
		{
			options.IsTransactional = false;
		});
	}

	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		context.Services.AddContextProvider().AddUnitOfWork();
	}
}
