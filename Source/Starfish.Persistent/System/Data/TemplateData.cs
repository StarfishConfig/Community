using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Persistent.Data;

/// <summary>
/// Represents the data for a message template, including its content, metadata, and lifecycle information.
/// </summary>
/// <remarks>Use this class to store and transfer information about message templates, such as email or phone
/// templates, including their identifiers, content, language, and audit details. The class includes properties for
/// tracking creation, updates, and deletion, which can be useful for auditing and versioning scenarios.</remarks>
internal class TemplateData : Persistent<long>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="TemplateData"/> class.
	/// </summary>
	public TemplateData()
	{
	}

	/// <summary>
	/// Initializes a new instance of the MessageTemplateData class with the specified template identifier.
	/// </summary>
	/// <param name="id">The unique identifier for the message template. Cannot be null or empty.</param>
	public TemplateData(long id)
		: base(id)
	{
	}

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

	/// <summary>
	/// Gets or sets a value indicating whether the template is active.
	/// </summary>
	public bool Active { get; set; }
}