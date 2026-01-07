using Microsoft.EntityFrameworkCore;
using Nerosoft.Euonia.Modularity;
using Nerosoft.Euonia.Repository;

namespace Nerosoft.Starfish.Repository;

[ConnectionString(Name = "Identity")]
internal class IdentityDataContext : DataContextWithBus<IdentityDataContext>
{
	public IdentityDataContext(DbContextOptions<IdentityDataContext> options, IRequestContextAccessor request)
		: base(options, request)
	{
	}
}