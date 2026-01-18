using Nerosoft.Euonia.Bus;
using Nerosoft.Starfish.Repository.Models;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Requests;

/// <summary>
/// Get message template detail by id
/// </summary>
/// <param name="Id"></param>
public record MessageTemplateDetailQuery(long Id) : IRequest<MessageTemplateDetailModel>;

/// <summary>
/// Represents a request to retrieve a list of message templates.
/// </summary>
/// <remarks>
/// This query is typically used to obtain all available message templates for display or selection
/// purposes. The result contains detailed information for each template. The returned list may be empty if no templates
/// are defined.
/// </remarks>
/// <param name="Code">The code or identifier used to filter the message templates. This parameter helps narrow down the list to templates matching the specified code.</param>
/// <param name="Type">The type or category of the message template to retrieve. This may refer to the format or delivery method (such as
/// 'Email', 'SMS', etc.).</param>
/// <param name="Keyword">A keyword to search for within the message templates. This can be used to filter templates based on content or metadata.</param>
/// <param name="Skip">The number of message templates to skip before starting to collect the result set. This is useful for pagination.</param>
/// <param name="Size">The maximum number of message templates to return in the result set. This is useful for pagination.</param>
public record MessageTemplateListQuery(string Code, TemplateType? Type, string Keyword, int Skip, int Size) : IRequest<IList<MessageTemplateListModel>>;

/// <summary>
/// Represents a query to retrieve the count of message templates that match the specified code, type, and keyword
/// criteria.
/// </summary>
/// <param name="Code">The code used to filter message templates. Only templates with a matching code will be included in the count. Can be
/// null or empty to ignore this filter.</param>
/// <param name="Type">The type of message template to filter by. If null, templates of all types are included.</param>
/// <param name="Keyword">A keyword to search for within message templates. Only templates containing this keyword will be counted. Can be
/// null or empty to ignore this filter.</param>
public record MessageTemplateCountQuery(string Code, TemplateType? Type, string Keyword) : IRequest<int>;

/// <summary>
/// Represents a query to retrieve a message template that matches the specified usage, type, and language.
/// </summary>
/// <param name="Code">The intended usage scenario for the message template. This value determines which template variant is selected (for
/// example, 'WelcomeEmail', 'PasswordReset', etc.). Cannot be null or empty.</param>
/// <param name="Type">The type or category of the message template to retrieve. This may refer to the format or delivery method (such as
/// 'Email', 'SMS', etc.). Cannot be null or empty.</param>
/// <param name="Language">The language code for the desired message template, specified in a standard format such as 'en-US' or 'fr-FR'.
/// Cannot be null or empty.</param>
public record MessageTemplateMatchQuery(string Code, TemplateType Type, string Language) : IRequest<MessageTemplateDetailModel>;