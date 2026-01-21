using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nerosoft.Euonia.Modularity;
using Nerosoft.Euonia.Repository;

namespace Nerosoft.Starfish.Repository;

[ConnectionString(Name = "System")]
internal class SystemDataContext : DataContextBase<SystemDataContext>
{
	public SystemDataContext(DbContextOptions<SystemDataContext> options, IRequestContextAccessor request, ILoggerFactory logger)
		: base(options, request, logger)
	{
	}
}