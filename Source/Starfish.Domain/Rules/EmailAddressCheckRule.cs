using Nerosoft.Euonia.Osba;
using Nerosoft.Starfish.Domain.Repositories;

namespace Nerosoft.Starfish.Domain.Rules;

/// <summary>
/// Represents a rule to check the availability of an email address during user updates.
/// </summary>
internal sealed class EmailAddressCheckRule(IPropertyInfo property)
	: RuleBase(property)
{
	/// <summary>
	/// Executes the rule to verify if the email address is available.
	/// </summary>
	/// <param name="context">The rule context containing the target object.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	public override async Task ExecuteAsync(IRuleContext context, CancellationToken cancellationToken = default)
	{
		// Ensure the target object is of type IEditableObject.
		if (context.Target is not IEditableObject target)
		{
			return;
		}

		var value = target.ReadProperty(Property)?.ToString();

		// Skip the rule if the email address is null or whitespace.
		if (string.IsNullOrWhiteSpace(value))
		{
			return;
		}

		var field = target.FieldManager.GetFieldData(Property);
		// Check if the email property has been changed.
		if (!field.IsChanged)
		{
			return;
		}

		// Check if the email address already exists for another user.

		var repository = target.BusinessContext.GetRequiredService<IUserRepository>();

		var exists = await repository.ExistsEmailAsync(value, cancellationToken);
		if (exists)
		{
			// Add an error result if the email address is unavailable.
			context.AddErrorResult(string.Format(IdentityResources.IDS_ERROR_EMAIL_ADDRESS_UNAVAILABLE, value));
		}
	}
}