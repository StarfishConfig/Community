namespace Nerosoft.Starfish.Facade.Transit;

/// <summary>
/// Data transfer object used to request a transfer of team ownership or membership.
/// </summary>
public class TeamTransferDto
{
	/// <summary>
	/// Gets or sets the identifier of the user to whom the team will be transferred.
	/// This should be the unique user id within the system.
	/// </summary>
	public string UserId { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether the current user should leave the team
	/// after the transfer is completed.
	/// True means the current user will be removed from the team after transfer;
	/// false means they will remain a member.
	/// </summary>
	public bool LeaveAfterTransfer { get; set; }
}