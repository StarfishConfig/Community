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
			.MaximumLength(50).WithMessage("Username cannot exceed 50 characters.")
			.Matches(RegexPattern.Username).WithMessage("Username format is invalid.");
	}
}
