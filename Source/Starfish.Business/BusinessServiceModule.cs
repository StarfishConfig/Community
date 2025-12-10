using Nerosoft.Euonia.Modularity;
using Nerosoft.Starfish.Domain;

namespace Nerosoft.Starfish.Business;

/// <summary>
/// 业务服务模块
/// </summary>
[DependsOn(typeof(DomainServiceModule))]
public class BusinessServiceModule : ModuleContextBase
{
}
