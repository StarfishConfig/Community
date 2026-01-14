using System.Security.Principal;
using Nerosoft.Euonia.Bus;
using Nerosoft.Euonia.Modularity;

namespace Nerosoft.Starfish.Facade;

internal class HybridIdentityProvider : IIdentityProvider
{
	private readonly IRequestContextAccessor _accessor;

	public HybridIdentityProvider(IRequestContextAccessor accessor)
	{
		_accessor = accessor;
	}

	public IPrincipal GetIdentity()
	{
		return _accessor.Context.User;
	}
}