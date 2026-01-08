using Microsoft.Extensions.Configuration;
using Nerosoft.Euonia.Business;
using Nerosoft.Starfish.Domain.Repositories;

namespace Nerosoft.Starfish.Domain.Rules;

/// <summary>
/// Represents a rule to check the availability of a username during user creation.
/// </summary>
internal sealed class UsernameCheckRule(IPropertyInfo property) : RuleBase(property)
{
	/// <summary>
	/// Executes the rule to verify if the username is available.
	/// </summary>
	/// <param name="context">The rule context containing the target object.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	public override async Task ExecuteAsync(IRuleContext context, CancellationToken cancellationToken = default)
	{
		// Ensure the target object is of type UserGeneralBusiness.
		if (context.Target is not IEditableObject target)
		{
			return;
		}

		// Skip the rule if the operation is not an insert.
		if (!target.IsNew)
		{
			return;
		}

		var value = target.ReadProperty(Property)?.ToString();

		if (string.IsNullOrWhiteSpace(value))
		{
			context.AddErrorResult(IdentityResources.IDS_ERROR_USERNAME_REQUIRED);
		}
		else
		{
			var configuration = target.BusinessContext.GetRequiredService<IConfiguration>();

			// Retrieve the list of reserved usernames from the configuration.
			var reserved = configuration.GetValue<List<string>>("ReservedUsernames");

			// Check if the username is in the reserved list.
			if (reserved?.Contains(value, StringComparer.InvariantCultureIgnoreCase) == true)
			{
				// Add an error result if the username is reserved.
				context.AddErrorResult(string.Format(IdentityResources.IDS_ERROR_USERNAME_UNAVAILABLE, value));
				return;
			}

			var repository = target.BusinessContext.GetRequiredService<IUserRepository>();

			// Check if the username already exists in the repository.
			var exists = await repository.ExistsUsernameAsync(value, cancellationToken);
			if (exists)
			{
				// Add an error result if the username already exists.
				context.AddErrorResult(string.Format(IdentityResources.IDS_ERROR_USERNAME_UNAVAILABLE, value));
			}
		}
	}
}