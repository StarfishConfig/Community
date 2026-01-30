using Nerosoft.Starfish.Infrastructure;

namespace Nerosoft.Starfish.Transit;

/// <summary>
/// Represents a data transfer object for requesting a one-time password.
/// </summary>
public class OnetimePasswordRequestDto
{
	/// <summary>
	/// Gets or sets the recipient of the one-time password (e.g., email address or phone number).
	/// </summary>
	public string Recipient { get; set; }

	/// <summary>
	/// Gets or sets the usage scenario for the one-time password.
	/// </summary>
	public OnetimePasswordUsage Usage { get; set; }

	/// <summary>
	/// Gets or sets the language code for the one-time password message.
	/// </summary>
	/// <value>
	/// - zh-Hans for Simplified Chinese
	/// - zh-Hant for Traditional Chinese
	/// - en for English
	/// - ja for Japanese
	/// - ko for Korean
	/// - fr for French
	/// - de for German
	/// - es for Spanish
	/// - it for Italian
	/// - ru for Russian
	/// - pt for Portuguese
	/// - vi for Vietnamese
	/// - id for Indonesian
	/// - tr for Turkish
	/// - nl for Dutch
	/// - sv for Swedish
	/// - fi for Finnish
	/// - no for Norwegian
	/// - da for Danish
	/// - hu for Hungarian
	/// </value>
	public string Language { get; set; }
}