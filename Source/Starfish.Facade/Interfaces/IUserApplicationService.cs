using Nerosoft.Euonia.Application;
using Nerosoft.Starfish.Facade.Transit;

namespace Nerosoft.Starfish.Facade.Interfaces;

public interface IUserApplicationService : IApplicationService
{
	Task<UserProfileDto> GetProfileAsync(CancellationToken cancellationToken = default);

	Task CreateAsync(UserCreateDto data, CancellationToken cancellationToken = default);

	Task UpdateAsync(UserUpdateDto data, CancellationToken cancellationToken = default);

	Task ChangePasswordAsync(UserPasswordChangeDto data, CancellationToken cancellationToken = default);

	Task ResetPasswordAsync(UserPasswordResetDto data, CancellationToken cancellationToken = default);

	Task ResetPasswordAsync(string userId, CancellationToken cancellationToken = default);
}