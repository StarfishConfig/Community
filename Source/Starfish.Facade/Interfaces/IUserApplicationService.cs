using Nerosoft.Euonia.Application;
using Nerosoft.Starfish.Facade.Transit;

namespace Nerosoft.Starfish.Facade.Interfaces;

public interface IUserApplicationService : IApplicationService
{
	Task<UserDetailDto> GetAsync(string id, CancellationToken cancellationToken = default);

	Task<List<UserListDto>> SearchAsync(string keywork, bool? locked, int skip, int size, CancellationToken cancellationToken = default);

	Task<int> CountAsync(string keywork, bool? locked, CancellationToken cancellationToken = default);

	Task<UserProfileDto> GetProfileAsync(CancellationToken cancellationToken = default);

	Task CreateAsync(UserCreateDto data, CancellationToken cancellationToken = default);

	Task UpdateAsync(string id, UserUpdateDto data, CancellationToken cancellationToken = default);

	Task UpdateProfileAsync(UserUpdateDto data, CancellationToken cancellationToken = default);

	Task ChangePasswordAsync(UserPasswordChangeDto data, CancellationToken cancellationToken = default);

	Task ResetPasswordAsync(UserPasswordResetDto data, CancellationToken cancellationToken = default);

	Task<string> ResetPasswordAsync(string id, CancellationToken cancellationToken = default);
}