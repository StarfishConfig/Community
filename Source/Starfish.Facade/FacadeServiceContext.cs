using Nerosoft.Euonia.Application;

namespace Nerosoft.Starfish.Facade;

internal class FacadeServiceContext : ServiceContextBase
{
	/// <inheritdoc/>
	public override bool AutoRegisterApplicationService => true;
}
