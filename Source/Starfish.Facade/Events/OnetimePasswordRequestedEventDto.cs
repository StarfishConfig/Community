using Nerosoft.Euonia.Domain;
using Nerosoft.Starfish.Infrastructure;

namespace Nerosoft.Starfish.Facade.Events;

internal class OnetimePasswordRequestedEventDto : ApplicationEvent
{
	public string Code { get; set; }

	public string Recipient { get; set; }

	public OnetimePasswordUsage Usage { get; set; }

	public int Duration { get; set; }

	public string Language { get; set; }
}
