using System.Collections.ObjectModel;
using Nerosoft.Euonia.Business;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Domain.Rules;

internal sealed class UserRoleCheckRule(IPropertyInfo property)
	: RuleBase(property)
{
	public override async Task ExecuteAsync(IRuleContext context, CancellationToken cancellationToken = default)
	{
		// Ensure the target object is of type IEditableObject.
		if (context.Target is not IEditableObject target)
		{
			return;
		}

		var values = (ObservableCollection<string>)target.ReadProperty(Property);

		if (values?.Count > 0)
		{
			foreach (var value in values)
			{
				if (RoleName.All.Contains(value))
				{
					continue;
				}

				context.AddErrorResult(string.Format(IdentityResources.IDS_ERROR_ROLE_NAME_INVALID, value));
			}
		}

		await Task.CompletedTask;
	}
}