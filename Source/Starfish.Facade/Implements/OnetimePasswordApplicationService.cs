using Microsoft.Extensions.Configuration;
using Nerosoft.Euonia.Application;
using Nerosoft.Starfish.Domain.Commands;
using Nerosoft.Starfish.Facade.Events;
using Nerosoft.Starfish.Facade.Interfaces;
using Nerosoft.Starfish.Toolkit;
using Nerosoft.Starfish.Transit;

namespace Nerosoft.Starfish.Facade.Implements;

internal class OnetimePasswordApplicationService(IConfiguration configuration)
	: BaseApplicationService, IOnetimePasswordApplicationService
{
	public async Task<string> RequestAsync(OnetimePasswordRequestDto data, CancellationToken cancellationToken = default)
	{
		var durationMinutes = configuration.GetValue($"OnetimePassword:Durations:{data.Usage}", 15);

		var code = GenerateRandomCode(6);

		var command = new OnetimePasswordCreateCommand
		{
			Recipient = data.Recipient,
			Usage = data.Usage,
			Code = code,
			RequestId = Guid.NewGuid().ToString(),
			Duration = TimeSpan.FromMinutes(durationMinutes)
		};

		await Bus.SendAsync(command, cancellationToken);

		var @event = new OnetimePasswordRequestedEventDto
		{
			Code = code,
			Recipient = data.Recipient,
			Usage = data.Usage,
			Duration = durationMinutes,
			Language = data.Language
		};

		await Bus.PublishAsync(@event, cancellationToken);

		return command.RequestId;
	}

	private static string GenerateRandomCode(int length)
	{
		const string chars = "0123456789";
		var random = new Random();
		return new string([.. Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)])]);
	}
}
