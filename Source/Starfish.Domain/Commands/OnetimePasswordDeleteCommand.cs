using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Domain.Commands;

/// <summary>
/// Represents a command to delete a one-time password request.
/// </summary>
public class OnetimePasswordDeleteCommand : Command
{
	/// <summary>
	/// Initializes a new instance of the OnetimePasswordDeleteCommand class with the specified request identifier.
	/// </summary>
	/// <param name="requestId">The unique identifier of the one-time password request to delete. Cannot be null.</param>
	public OnetimePasswordDeleteCommand(string requestId)
	{
		RequestId = requestId;
	}

	/// <summary>
	/// Gets or sets the unique identifier of the one-time password request to be deleted.
	/// </summary>
	public string RequestId { get; set; }
}