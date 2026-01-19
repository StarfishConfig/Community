using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Domain.Commands;

public class TeamMemberRemoveCommand : Command
{
	public TeamMemberRemoveCommand(long id, string userId)
	{
		Id = id;
		UserId = userId;
	}

	public long Id { get; }

	public string UserId { get; }
}