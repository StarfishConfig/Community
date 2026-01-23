using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Domain.Commands;

/// <summary>
/// Represents a command to check off (mark as used) a one-time password.
/// </summary>
public class OnetimePasswordCheckOffCommand : Command
{
	/// <summary>
	/// Initializes a new instance of the OnetimePasswordCheckOffCommand class with the specified request identifier and checked timestamp.
	/// </summary>
	/// <param name="requestId">The unique identifier of the one-time password request to write off. Cannot be null.</param>
	/// <param name="checkedAt">The timestamp when the one-time password was checked or verified.</param>
	public OnetimePasswordCheckOffCommand(string requestId, DateTime checkedAt)
	{
		RequestId = requestId;
		CheckedAt = checkedAt;
	}

	/// <summary>
	/// Gets or sets the unique identifier of the one-time password request.
	/// </summary>
	public string RequestId { get; set; }

	/// <summary>
	/// Gets or sets the timestamp when the one-time password was checked or verified.
	/// </summary>
	public DateTime CheckedAt { get; set; }
}