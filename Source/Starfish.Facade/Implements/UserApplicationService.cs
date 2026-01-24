using Nerosoft.Euonia.Application;
using Nerosoft.Euonia.Mapping;
using Nerosoft.Starfish.Domain.Commands;
using Nerosoft.Starfish.Facade.Interfaces;
using Nerosoft.Starfish.Transit;
using Nerosoft.Starfish.Repository.Requests;
using Nerosoft.Starfish.Toolkit;

namespace Nerosoft.Starfish.Facade.Implements;

internal class UserApplicationService : BaseApplicationService, IUserApplicationService
{
	public Task<UserDetailDto> GetAsync(string id, CancellationToken cancellationToken = default)
	{
		var request = new UserDetailQuery(id);
		return Bus.CallAsync(request, cancellationToken)
				  .ContinueWith(task =>
				  {
					  task.WaitAndUnwrapException(cancellationToken);
					  return TypeAdapter.ProjectedAs<UserDetailDto>(task.Result);
				  }, cancellationToken);
	}

	public Task<List<UserListDto>> SearchAsync(string keywork, bool? locked, int skip, int size, CancellationToken cancellationToken = default)
	{
		var request = new UserSearchQuery(keywork, locked, skip, size);
		return Bus.CallAsync(request, cancellationToken)
				  .ContinueWith(task =>
				  {
					  task.WaitAndUnwrapException(cancellationToken);
					  return TypeAdapter.ProjectedAs<List<UserListDto>>(task.Result);
				  }, cancellationToken);
	}

	public Task<int> CountAsync(string keywork, bool? locked, CancellationToken cancellationToken = default)
	{
		var request = new UserCountQuery(keywork, locked);
		return Bus.CallAsync(request, cancellationToken);
	}

	public Task<List<UserLookupDto>> LookupAsync(string keyword, int size, CancellationToken cancellationToken = default)
	{
		var request = new UserSearchQuery(keyword, null, 0, size);
		return Bus.CallAsync(request, cancellationToken)
				  .ContinueWith(task => TypeAdapter.ProjectedAs<List<UserLookupDto>>(task.Result), cancellationToken);
	}

	public Task<UserProfileDto> GetProfileAsync(CancellationToken cancellationToken = default)
	{
		var request = new UserDetailQuery(User.UserId);
		return Bus.CallAsync(request, cancellationToken)
				  .ContinueWith(task => TypeAdapter.ProjectedAs<UserProfileDto>(task.Result), cancellationToken);
	}

	public Task CreateAsync(UserCreateDto data, CancellationToken cancellationToken = default)
	{
		var command = TypeAdapter.ProjectedAs<UserCreateCommand>(data);
		return Bus.SendAsync(command, cancellationToken);
	}

	public Task UpdateAsync(string id, UserUpdateDto data, CancellationToken cancellationToken = default)
	{
		var command = new UserUpdateCommand(id);
		TypeAdapter.ProjectedAs(data, command);
		return Bus.SendAsync(command, cancellationToken);
	}

	public Task UpdateProfileAsync(UserUpdateDto data, CancellationToken cancellationToken = default)
	{
		var command = new UserUpdateCommand(User.UserId);
		TypeAdapter.ProjectedAs(data, command);
		return Bus.SendAsync(command, cancellationToken);
	}

	public Task ChangePasswordAsync(UserPasswordChangeDto data, CancellationToken cancellationToken = default)
	{
		var userId = User.UserId;

		var request = new UserPasswordVerifyRequest(userId, data.OldPassword);

		var isOldPasswordValid = Bus.CallAsync(request, cancellationToken).GetAwaiter().GetResult();

		if (!isOldPasswordValid)
		{
			throw new ForbiddenException("The old password is incorrect.");
		}

		var command = new UserPasswordChangeCommand(User.UserId, data.NewPassword);
		return Bus.SendAsync(command, cancellationToken);
	}

	public Task ResetPasswordAsync(UserPasswordResetDto data, CancellationToken cancellationToken = default)
	{
		var userId = string.Empty;

		var command = new UserPasswordResetCommand(userId, data.Password);
		return Bus.SendAsync(command, cancellationToken);
	}

	public async Task<string> ResetPasswordAsync(string id, CancellationToken cancellationToken = default)
	{
		const PasswordComplexity complexity = PasswordComplexity.ContainsUppercase | PasswordComplexity.ContainsLowercase |
											  PasswordComplexity.ContainsDigit | PasswordComplexity.ContainsSymbol;
		var password = PasswordGenerator.GeneratePassword(complexity, 12, 24);
		var command = new UserPasswordResetCommand(id, password)
		{
			ChangedBy = User.UserId
		};
		await Bus.SendAsync(command, cancellationToken);
		return password;
	}
}