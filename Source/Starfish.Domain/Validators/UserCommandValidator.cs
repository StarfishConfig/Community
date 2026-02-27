using FluentValidation;
using Nerosoft.Starfish.Domain.Commands;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Domain.Validators;

internal class UserCreateCommandValidator : AbstractValidator<UserCreateCommand>
{
	public UserCreateCommandValidator()
	{
		RuleFor(x => x.Username)
			.NotEmpty().WithMessage("Username is required.")
			.MinimumLength(LengthConstraints.UsernameMinimumLength).WithMessage("Username must be at least 4 characters.")
			.MaximumLength(LengthConstraints.UsernameMaximumLength).WithMessage("Username cannot exceed 50 characters.")
			.Matches(RegexPattern.Username).WithMessage("Username format is invalid.");

		RuleFor(x => x.Email)
			.NotEmpty().WithMessage("Email is required.")
			.EmailAddress().WithMessage("Email format is invalid.")
			.MaximumLength(LengthConstraints.EmailMaximumLength).WithMessage("Email cannot exceed 100 characters.");

		RuleFor(x => x.Password)
			.NotEmpty().WithMessage("Password is required.")
			.MinimumLength(LengthConstraints.PasswordMinimumLength).WithMessage($"Password must be at least {LengthConstraints.PasswordMinimumLength} characters.")
			.MaximumLength(LengthConstraints.PasswordMaximumLength).WithMessage($"Password cannot exceed {LengthConstraints.PasswordMaximumLength} characters.");

		RuleFor(x => x.Nickname)
			.MaximumLength(LengthConstraints.NicknameMaximumLength).WithMessage($"Nickname cannot exceed {LengthConstraints.NicknameMaximumLength} characters.");
	}
}