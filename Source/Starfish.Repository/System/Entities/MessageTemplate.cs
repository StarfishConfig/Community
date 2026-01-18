using Nerosoft.Euonia.Repository;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Entities;

/// <summary>
/// Represents a message template entity.
/// </summary>
internal class MessageTemplate : Entity<long>, IAuditable
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

	/// <summary>
	/// Gets or sets the creation time of the template.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the user who created the template.
	/// </summary>
	public string CreatedBy { get; set; }

	/// <summary>
	/// Gets or sets the last update time of the template.
	/// </summary>
	public DateTime UpdatedAt { get; set; }

	/// <summary>
	/// Gets or sets the user who last updated the template.
	/// </summary>
	public string UpdatedBy { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether the template is deleted.
	/// </summary>
	public bool IsDeleted { get; set; }

	/// <summary>
	/// Gets or sets the deletion time of the template.
	/// </summary>
	public DateTime? DeletedAt { get; set; }

	/// <summary>
	/// Gets or sets the user who deleted the template.
	/// </summary>
	public string DeletedBy { get; set; }
}