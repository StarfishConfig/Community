using Nerosoft.Euonia.Application;
using Nerosoft.Euonia.Mapping;
using Nerosoft.Starfish.Domain.Commands;
using Nerosoft.Starfish.Facade.Interfaces;
using Nerosoft.Starfish.Facade.Transit;
using Nerosoft.Starfish.Repository.Requests;
using Nerosoft.Starfish.Toolkit;

namespace Nerosoft.Starfish.Facade.Implements;

internal class UserApplicationService : BaseApplicationService, IUserApplicationService
{
	public Task<UserProfileDto> GetProfileAsync(CancellationToken cancellationToken = default)
	{
		var request = new UserDetailQueryRequest(User.UserId);
		return Bus.CallAsync(request, cancellationToken)
		          .ContinueWith(task => TypeAdapter.ProjectedAs<UserProfileDto>(task.Result), cancellationToken);
	}

	public Task CreateAsync(UserCreateDto data, CancellationToken cancellationToken = default)
	{
		var command = TypeAdapter.ProjectedAs<UserCreateCommand>(data);
		return Bus.SendAsync(command, cancellationToken);
	}

	public Task UpdateAsync(UserUpdateDto data, CancellationToken cancellationToken = default)
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

	public Task ResetPasswordAsync(string userId, CancellationToken cancellationToken = default)
	{
		const PasswordComplexity complexity = PasswordComplexity.ContainsUppercase | PasswordComplexity.ContainsLowercase |
		                                      PasswordComplexity.ContainsDigit | PasswordComplexity.ContainsSymbol;
		var password = PasswordGenerator.GeneratePassword(complexity, 12, 16);
		var command = new UserPasswordResetCommand(userId, password)
		{
			ChangedBy = User.UserId
		};
		return Bus.SendAsync(command, cancellationToken);
	}
}