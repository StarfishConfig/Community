using Nerosoft.Euonia.Repository;

namespace Nerosoft.Starfish.Repository.Entities;

/// <summary>
/// Represents a third-party user authority entity in the persistence layer.
/// </summary>
internal class UserAuthority : Entity<long>, IHasCreateTime
{
	/// <summary>
	/// Gets or sets the user ID associated with this authority.
	/// </summary>
	public string UserId { get; set; }

	/// <summary>
	/// Gets or sets the provider of the third-party authentication.
	/// </summary>
	public string Provider { get; set; }

	/// <summary>
	/// Gets or sets the unique identifier for the user on the third-party platform.
	/// </summary>
	public string OpenId { get; set; }

	/// <summary>
	/// Gets or sets the name of the user on the third-party platform.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the creation time of this authority record.
	/// </summary>
	public DateTime CreatedAt { get; set; }
	
	/// <summary>
	/// Gets or sets the associated user entity.
	/// </summary>
	public User User { get; set; }
}