using Nerosoft.Euonia.Hosting;
using Nerosoft.Euonia.Modularity;
using Nerosoft.Starfish.Facade;

namespace Nerosoft.Starfish.Server;

[DependsOn(typeof(HostingModule), typeof(FacadeServiceModule))]
internal class ServerStartupModule : ModuleContextBase
{
}
