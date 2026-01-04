using Nerosoft.Euonia.Repository;

namespace Nerosoft.Starfish.Persist;

/// <summary>
/// Represents an interface for auditable entities.
/// </summary>
/// <remarks>
/// This interface inherits from <see cref="IAuditable{TUserKey}"/> and uses <see cref="string"/> as the user key type.
/// Implementing this interface enables automatic tracking of entity creation and modification metadata,
/// including the user who performed the operation and the timestamp.
/// </remarks>
public interface IAuditable : IAuditable<string>
{
}