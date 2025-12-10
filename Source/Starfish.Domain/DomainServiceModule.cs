using Nerosoft.Euonia.Mapping;
using Nerosoft.Euonia.Modularity;
using Nerosoft.Euonia.Validation;

namespace Nerosoft.Starfish.Domain;

/// <summary>
/// 领域服务模块
/// </summary>
[DependsOn(typeof(AutomapperModule), typeof(ValidationModule))]
internal class DomainServiceModule : ModuleContextBase
{ 
	
}
