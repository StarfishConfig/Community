using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Facade.Transit;

/// <summary>
/// Message template criteria for querying message templates.
/// </summary>
public class TemplateCriteria
{
	/// <summary>
	/// Gets or sets the code associated with this instance.
	/// </summary>
	public string Code { get; set; }

	/// <summary>
	/// Gets or sets the type of the message template.
	/// </summary>
	public TemplateType? Type { get; set; }

	/// <summary>
	/// Gets or sets the keyword used for searching or filtering operations.
	/// </summary>
	public string Keyword { get; set; }
}
