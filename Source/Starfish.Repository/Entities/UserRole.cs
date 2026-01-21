using Nerosoft.Euonia.Repository;

namespace Nerosoft.Starfish.Repository.Entities;

/// <summary>
/// Represents a user role entity in the persistence layer.
/// </summary>
internal class UserRole : Entity<string>, IHasCreateTime
{
	/// <summary>
	/// Initializes a new instance of the <see cref="UserRole"/> class.
	/// This constructor is private to enforce controlled creation of instances.
	/// </summary>
	private UserRole()
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="UserRole"/> class with the specified role name.
	/// </summary>
	/// <param name="name"></param>
	public UserRole(string name)
		: this()
	{
		Name = name;
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
	public User User { get; set; }
}