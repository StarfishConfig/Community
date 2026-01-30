using Nerosoft.Euonia.Domain;
using Nerosoft.Starfish.Infrastructure;

namespace Nerosoft.Starfish.Domain.Commands;

/// <summary>
/// Represents a command to create a one-time password.
/// </summary>
public class OnetimePasswordCreateCommand : Command
{
	/// <summary>
	/// Gets or sets the unique identifier for the one-time password request.
	/// </summary>
	public string RequestId { get; set; }

	/// <summary>
	/// Gets or sets the code of the one-time password.
	/// </summary>
	public string Code { get; set; }

	/// <summary>
	/// Gets or sets the recipient of the one-time password.
	/// </summary>
	public string Recipient { get; set; }

	/// <summary>
	/// Gets or sets the expiration time of the one-time password.
	/// </summary>
	public DateTime? Expiration { get; set; }

	/// <summary>
	/// Gets or sets the effective duration of the one-time password.
	/// </summary>
	public TimeSpan? Duration { get; set; }

	/// <summary>
	/// Gets or sets the usage context of the one-time password.
	/// </summary>
	public OnetimePasswordUsage Usage { get; set; }
}
