using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nerosoft.Euonia.Modularity;
using Nerosoft.Euonia.Repository;

namespace Nerosoft.Starfish.Repository;

[ConnectionString(Name = "Project")]
internal class ProjectDataContext : DataContextBase<ProjectDataContext>
{
	public ProjectDataContext(DbContextOptions<ProjectDataContext> options, IRequestContextAccessor request, ILoggerFactory logger)
		: base(options, request, logger)
	{
	}
}