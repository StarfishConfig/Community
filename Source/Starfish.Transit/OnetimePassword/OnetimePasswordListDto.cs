using Nerosoft.Starfish.Infrastructure;

namespace Nerosoft.Starfish.Transit;

public class OnetimePasswordListDto
{
	/// <summary>
	/// Gets or sets the unique identifier of the one-time password.
	/// </summary>
	public long Id { get; set; }

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
	/// Gets or sets the usage type of the one-time password.
	/// </summary>
	public OnetimePasswordUsage Usage { get; set; }

	/// <summary>
	/// Gets or sets the creation time of the one-time password.
	/// </summary>
	public DateTime CreatedAt { get; set; }
}
