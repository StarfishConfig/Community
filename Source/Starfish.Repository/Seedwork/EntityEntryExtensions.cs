using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Nerosoft.Starfish.Repository;

internal static class EntityEntryExtensions
{
	public static void TrySetProperty<TProperty>(this EntityEntry entry, string propertyName, TProperty value)
	{
		if (entry.Properties.Any(p => p.Metadata.Name == propertyName))
		{
			entry.Property(propertyName).CurrentValue = value;
		}
	}
}