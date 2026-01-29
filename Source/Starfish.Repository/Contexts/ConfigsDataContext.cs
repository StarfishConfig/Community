using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nerosoft.Euonia.Modularity;
using Nerosoft.Euonia.Repository;

namespace Nerosoft.Starfish.Repository;

[ConnectionString(Name = "Configs")]
internal class ConfigsDataContext : DataContextBase<ConfigsDataContext>
{
	public ConfigsDataContext(DbContextOptions<ConfigsDataContext> options, IRequestContextAccessor request, ILoggerFactory logger)
		: base(options, request, logger)
	{
	}
}