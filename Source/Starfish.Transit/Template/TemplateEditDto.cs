using Nerosoft.Starfish.Infrastructure;

namespace Nerosoft.Starfish.Transit;

/// <summary>
/// Represents a data transfer object for editing message templates.
/// </summary>
public class TemplateEditDto
{
	/// <summary>
	/// Gets or sets the name of the template.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the code of the template.
	/// </summary>
	/// <remarks>
	/// The code represents the usage scenario for the message template, determining which variant is selected.
	/// </remarks>
	public string Code { get; set; }

	/// <summary>
	/// Gets or sets the language of the template.
	/// </summary>
	public string Language { get; set; }

	/// <summary>
	/// Gets or sets the type of the template.
	/// </summary>
	/// <value>Possible values include "email" and "phone".</value>
	public TemplateType Type { get; set; }

	/// <summary>
	/// Gets or sets the subject of the template.
	/// </summary>
	public string Subject { get; set; }

	/// <summary>
	/// Gets or sets the message body of the template.
	/// </summary>
	public string Body { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether the template is the default one for its usage and type.
	/// </summary>
	public bool Default { get; set; }
}
