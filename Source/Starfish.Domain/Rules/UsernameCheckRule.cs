using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Nerosoft.Euonia.Osba;
using Nerosoft.Starfish.Domain.Repositories;
using Nerosoft.Starfish.Shared;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

		// Use PriorityValueFinder to check various conditions for username validity.
		var error = await PriorityValueFinder.FindAsync<string>(queue =>
		{
			// Check if the username is null or whitespace.
			queue.Enqueue(() => Task.FromResult(string.IsNullOrWhiteSpace(value) ? IdentityResources.IDS_ERROR_USERNAME_REQUIRED : null), 1);
			// Check if the username is matches the format rules
			queue.Enqueue(() => Task.FromResult(!Regex.IsMatch(value, RegexPattern.Username) ? string.Format(IdentityResources.IDS_ERROR_USERNAME_UNAVAILABLE, value) : null), 2);
			// Check if the username is in the reserved list.
			queue.Enqueue(async () =>
			{
				var configuration = target.BusinessContext.GetRequiredService<IConfiguration>();
				// Retrieve the list of reserved usernames from the configuration.
				var reserved = configuration.GetValue<List<string>>("ReservedUsernames");

				return reserved?.Contains(value, StringComparer.InvariantCultureIgnoreCase) == true ? string.Format(IdentityResources.IDS_ERROR_USERNAME_UNAVAILABLE, value) : null;
			}, 3);
			// Check if the username already exists in the repository.
			queue.Enqueue(async () =>
			{
				// Get the user repository from the business context.
				var repository = target.BusinessContext.GetRequiredService<IUserRepository>();

				// Check if the username already exists in the repository.
				var exists = await repository.ExistsUsernameAsync(value, cancellationToken);

				// Return an error result if the username already exists.
				return exists ? string.Format(IdentityResources.IDS_ERROR_USERNAME_UNAVAILABLE, value) : null;
			}, 4);
		}, v => !string.IsNullOrEmpty(v));

		if (!string.IsNullOrEmpty(error))
		{
			context.AddErrorResult(error);
		}
	}
}