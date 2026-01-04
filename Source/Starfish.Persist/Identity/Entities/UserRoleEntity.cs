using Nerosoft.Euonia.Repository;

namespace Nerosoft.Starfish.Persist.Entities;

/// <summary>
/// Represents a user role entity in the persistence layer.
/// </summary>
internal class UserRoleEntity : PersistentBase<string>, IHasCreateTime
{
	/// <summary>
	/// Initializes a new instance of the <see cref="UserRoleEntity"/> class.
	/// This constructor is private to enforce controlled creation of instances.
	/// </summary>
	private UserRoleEntity()
	{
	}

	/// <summary>
	/// Gets or sets the name of the role.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the ID of the user associated with this role.
	/// </summary>
	public string UserId { get; set; }

	/// <summary>
	/// Gets or sets the creation time of the role.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the user associated with this role.
	/// </summary>
	public UserEntity User { get; set; }
}