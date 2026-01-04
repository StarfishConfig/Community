using Microsoft.Extensions.DependencyInjection;
using Nerosoft.Euonia.Mapping;
using Nerosoft.Euonia.Modularity;
using Nerosoft.Euonia.Validation;

namespace Nerosoft.Starfish.Domain;

/// <summary>
/// Registers domain-level services for the bounded context.
/// </summary>
/// <remarks>
/// This module is responsible for composing and registering domain services, validators,
/// and mapping profiles required by the domains. It declares dependencies on
/// <see cref="AutomapperModule"/> and <see cref="ValidationModule"/>, ensuring that
/// mapping and validation infrastructure is available when this module is initialized.
/// </remarks>
/// <seealso cref="ModuleContextBase"/>
[DependsOn(typeof(AutomapperModule), typeof(ValidationModule))]
public class DomainServiceModule : ModuleContextBase
{
	/// <summary>
	/// Add services to the container.
	/// </summary>
	/// <param name="context"></param>
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		context.Services.AddBusinessObject(typeof(DomainServiceModule).Assembly);
		context.Services.AddTransient(typeof(ShortUniqueIdResolver<,>));
	}
}