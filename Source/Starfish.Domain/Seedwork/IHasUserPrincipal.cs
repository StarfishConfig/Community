using Nerosoft.Euonia.Claims;

namespace Nerosoft.Starfish.Domain;

/// <summary>
/// Defines a contract for objects that have an associated user principal.
/// </summary>
public interface IHasUserPrincipal
{
	/// <summary>
	/// Gets the user principal representing the identity of the current user.
	/// </summary>
	/// <value>
	/// The <see cref="UserPrincipal"/> instance containing the user's identity information.
	/// </value>
	UserPrincipal Identity { get; }
}