using Nerosoft.Euonia.Application;
using Nerosoft.Euonia.Modularity;
using Nerosoft.Starfish.Business;
using Nerosoft.Starfish.Domain;
using Nerosoft.Starfish.Persist;

namespace Nerosoft.Starfish.Facade;

/// <summary>
/// 应用服务模块
/// </summary>
[DependsOn(typeof(ApplicationModule))]
[DependsOn(typeof(PersistServiceModule), typeof(BusinessServiceModule), typeof(DomainServiceModule))]
public class FacadeServiceModule : ModuleContextBase
{
}
