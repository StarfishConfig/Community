using Nerosoft.Euonia.Hosting;
using Nerosoft.Euonia.Modularity;
using Nerosoft.Starfish.Facade;

namespace Nerosoft.Starfish.Server;

[DependsOn(typeof(HostingModule), typeof(FacadeServiceModule))]
internal class ServerStartupModule : ModuleContextBase
{
	/// <summary>
	/// Add services to the container.
	/// </summary>
	/// <param name="context"></param>
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		context.Services.AddGrpc(options =>
		{
			options.EnableDetailedErrors = true;
			options.MaxReceiveMessageSize = null;
			options.MaxSendMessageSize = null;
			options.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.SmallestSize;
			options.ResponseCompressionAlgorithm = "gzip";
		});

		context.Services.AddRazorComponents()
						.AddInteractiveServerComponents();
	}
}
