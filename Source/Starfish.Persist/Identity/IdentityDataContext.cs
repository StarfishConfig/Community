using Microsoft.EntityFrameworkCore;
using Nerosoft.Euonia.Modularity;
using Nerosoft.Euonia.Repository;

namespace Nerosoft.Starfish.Persist;

[ConnectionString(Name = "Identity")]
internal class IdentityDataContext : DataContextWithBus<IdentityDataContext>
{
	public IdentityDataContext(DbContextOptions<IdentityDataContext> options, IRequestContextAccessor request)
		: base(options, request)
	{
	}
}