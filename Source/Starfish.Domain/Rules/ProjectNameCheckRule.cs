using Nerosoft.Euonia.Osba;
using Nerosoft.Starfish.Persistent.Repositories;

namespace Nerosoft.Starfish.Domain.Rules;

/// <summary>
/// Validates that a project name is provided and is unique within the specified team.
/// </summary>
/// <param name="property">The property information for the project name being validated.</param>
/// <remarks>
/// This rule performs two validations:
/// <list type="bullet">
/// <item><description>Ensures the project name is not null or whitespace.</description></item>
/// <item><description>Verifies that the project name is unique within the team by checking against existing projects.</description></item>
/// </list>
/// The rule uses the <see cref="IProjectRepository"/> to check for name duplication within the team scope.
/// </remarks>
internal sealed class ProjectNameCheckRule(IPropertyInfo property) : RuleBase(property)
{
	/// <summary>
	/// Executes the project name validation rule asynchronously.
	/// </summary>
	/// <param name="context">The rule context containing the target object and validation state.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A task representing the asynchronous validation operation.</returns>
	/// <remarks>
	/// The validation process:
	/// <list type="number">
	/// <item><description>Verifies the target object implements <see cref="IEditableObject"/>.</description></item>
	/// <item><description>Checks if the name property value is null or whitespace and adds an error if so.</description></item>
	/// <item><description>If a valid team ID exists, queries the repository to check for duplicate names within that team.</description></item>
	/// <item><description>Adds an error result if a duplicate name is found, excluding the current project (by ID).</description></item>
	/// </list>
	/// </remarks>
	public async override Task ExecuteAsync(IRuleContext context, CancellationToken cancellationToken = default)
	{
		// Ensure the target object is of type UserGeneralBusiness.
		if (context.Target is not IEditableObject target)
		{
			return;
		}

		var value = target.ReadProperty(Property) as string;

		if (string.IsNullOrWhiteSpace(value))
		{
			context.AddErrorResult(ProjectResources.IDS_ERROR_NAME_REQUIRED);
		}
		else
		{
			var teamId = target.ReadProperty<long>("TeamId");
			var id = target.ReadProperty<long>("Id");

			if (teamId > 0)
			{
				var repository = target.BusinessContext.GetRequiredService<IProjectRepository>();
				if (repository != null)
				{
					var exists = await repository.CheckNameDuplicationAsync(teamId, value, id, cancellationToken);

					if (exists)
					{
						context.AddErrorResult(ProjectResources.IDS_ERROR_NAME_DUPLICATED);
					}
				}
			}
		}
	}
}