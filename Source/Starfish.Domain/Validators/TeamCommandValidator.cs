using FluentValidation;
using Nerosoft.Starfish.Domain.Commands;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Domain.Validators;

internal class TeamCreateCommandValidator : AbstractValidator<TeamCreateCommand>
{
	public TeamCreateCommandValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty().WithMessage(TeamResources.IDS_ERROR_NAME_REQUIRED)
			.MaximumLength(TeamConstants.NameMaximumLength).WithMessage("Team name cannot exceed 100 characters.");

		RuleFor(x => x.Description)
			.MaximumLength(TeamConstants.DescriptionMaximumLength).WithMessage("Team description cannot exceed 500 characters.");
	}
}

internal class TeamUpdateCommandValidator : AbstractValidator<TeamUpdateCommand>
{
	public TeamUpdateCommandValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty().WithMessage(TeamResources.IDS_ERROR_NAME_REQUIRED)
			.MaximumLength(TeamConstants.NameMaximumLength).WithMessage("Team name cannot exceed 100 characters.");

		RuleFor(x => x.Description)
			.MaximumLength(TeamConstants.DescriptionMaximumLength).WithMessage("Team description cannot exceed 500 characters.");
	}
}