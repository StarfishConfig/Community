using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Domain.Commands;

/// <summary>
/// Represents a command to reset the failed login attempt count for a user.
/// </summary>
public class UserFailedCountChangeCommand : Command
{
	/// <summary>
	/// Initializes a new instance of the <see cref="UserFailedCountChangeCommand"/> class.
	/// </summary>
	/// <param name="id">The unique identifier of the user whose failed login count is to be reset.</param>
	/// <param name="actionType">The action type.</param>
	public UserFailedCountChangeCommand(string id,string actionType)
	{
		Id = id;
		ActionType = actionType;
	}

	/// <summary>
	/// Gets the unique identifier of the user.
	/// </summary>
	public string Id { get; private set; }
	
	/// <summary>
	/// Gets or sets the type of action that triggered the failed count change.
	/// </summary>
	/// <value>
	/// <para>Increase</para>
	/// <para>Decrease</para>
	/// </value>
	public string ActionType { get; set; }
}