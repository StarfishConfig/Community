using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Persistent.Data;

/// <summary>
/// Represents the data structure for a one-time password.
/// </summary>
internal class OnetimePasswordData : Persistent<long>
{
	/// <summary>
	/// Gets or sets the unique identifier for the one-time password request.
	/// </summary>
	public string RequestId { get; set; }

	/// <summary>
	/// Gets or sets the code of the one-time password.
	/// </summary>
	public string Code { get; set; }

	/// <summary>
	/// Gets or sets the recipient of the one-time password.
	/// </summary>
	public string Recipient { get; set; }

	/// <summary>
	/// Gets or sets the expiration time of the one-time password.
	/// </summary>
	public DateTime? Expiration { get; set; }

	/// <summary>
	/// Gets or sets the time when the one-time password was checked.
	/// </summary>
	public DateTime? Checked { get; set; }

	/// <summary>
	/// Gets or sets the effective duration of the one-time password in seconds.
	/// </summary>
	public int? Duration { get; set; }

	/// <summary>
	/// Gets or sets the usage context of the one-time password.
	/// </summary>
	public OnetimePasswordUsage Usage { get; set; }
}
