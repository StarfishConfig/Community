using Nerosoft.Euonia.Domain;

namespace Nerosoft.Starfish.Domain;

/// <summary>
/// Represents an interface for auditable entities.
/// </summary>
/// <remarks>
/// This interface inherits from <see cref="IAuditing{TUserKey}"/> and uses <see cref="string"/> as the user key type.
/// Implementing this interface enables automatic tracking of entity creation and modification metadata,
/// including the user who performed the operation and the timestamp.
/// </remarks>
public interface IAuditing : IAuditing<string>
{
}