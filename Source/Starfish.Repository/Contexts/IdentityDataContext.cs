using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nerosoft.Euonia.Modularity;
using Nerosoft.Euonia.Repository;

namespace Nerosoft.Starfish.Repository;

[ConnectionString(Name = "Identity")]
internal class IdentityDataContext : DataContextBase<IdentityDataContext>
{
	public IdentityDataContext(DbContextOptions<IdentityDataContext> options, IRequestContextAccessor request, ILoggerFactory logger)
		: base(options, request, logger)
	{
	}
}