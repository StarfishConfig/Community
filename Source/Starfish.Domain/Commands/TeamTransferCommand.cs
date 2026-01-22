using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Domain.Commands;

/// <summary>
/// Command to transfer ownership of a team to another user.
/// Inherits from <see cref="Command"/> which provides command metadata (e.g., correlation id).
/// </summary>
public class TeamTransferCommand : Command
{
	/// <summary>
	/// Initializes a new instance of the <see cref="TeamTransferCommand"/> class.
	/// </summary>
	/// <param name="id">The unique numeric identifier of the team to transfer.</param>
	/// <param name="ownerId">The identifier of the user who will become the new owner.</param>
	public TeamTransferCommand(long id, string ownerId)
	{
		Id = id;
		OwnerId = ownerId;
	}

	/// <summary>
	/// Gets the unique identifier of the team being transferred.
	/// </summary>
	public long Id { get; }

	/// <summary>
	/// Gets the identifier of the new owner (user id) after the transfer.
	/// </summary>
	public string OwnerId { get; }

	/// <summary>
	/// Gets or sets a value indicating whether the current owner should leave the team after the transfer.
	/// When <c>true</c>, the previous owner will be removed from the team's membership following transfer.
	/// </summary>
	public bool LeaveAfterTransfer { get; set; }
}