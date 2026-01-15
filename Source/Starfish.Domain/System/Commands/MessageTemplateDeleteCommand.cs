using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Domain.Commands;

/// <summary>
/// Represents a command to delete an existing message template.
/// </summary>
public class MessageTemplateDeleteCommand : Command
{
	/// <summary>
	/// Initializes a new instance of the MessageTemplateDeleteCommand class with the specified template identifier.
	/// </summary>
	/// <param name="id">The unique identifier of the message template to delete. Cannot be null.</param>
	public MessageTemplateDeleteCommand(long id)
	{
		Id = id;
	}

	/// <summary>
	/// Gets the identifier of the template to be deleted.
	/// </summary>
	public long Id { get; }
}