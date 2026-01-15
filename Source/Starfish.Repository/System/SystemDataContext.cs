using Microsoft.EntityFrameworkCore;
using Nerosoft.Euonia.Modularity;
using Nerosoft.Euonia.Repository;

namespace Nerosoft.Starfish.Repository;

[ConnectionString(Name = "System")]
internal class SystemDataContext : DataContextBase<SystemDataContext>
{
	public SystemDataContext(DbContextOptions<SystemDataContext> options, IRequestContextAccessor request)
		: base(options, request)
	{
	}
}
