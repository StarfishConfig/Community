using Nerosoft.Euonia.Repository;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Entities;

/// <summary>
/// Represents a one-time password entity.
/// </summary>
internal class OnetimePassword : Entity<long>, IHasCreateTime
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
	/// Gets or sets the usage type of the one-time password.
	/// </summary>
	public OnetimePasswordUsage Usage { get; set; }

	/// <summary>
	/// Gets or sets the creation time of the one-time password.
	/// </summary>
	public DateTime CreatedAt { get; set; }
}
