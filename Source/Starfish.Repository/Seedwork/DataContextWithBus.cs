using System.Diagnostics;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Nerosoft.Euonia.Modularity;
using Nerosoft.Euonia.Repository;
using Nerosoft.Euonia.Repository.EfCore;

namespace Nerosoft.Starfish.Repository;

/// <summary>
/// Base data context with bus and request context support.
/// </summary>
internal abstract class DataContextWithBus<TContext> : DataContextBase<TContext>
	where TContext : DbContext, IRepositoryContext
{
	private readonly List<object> _unchangedEntities = [];
	private readonly IRequestContextAccessor _request;

	/// <summary>
	/// Initializes a new instance of the <see cref="DataContextWithBus{TContext}"/> class.
	/// </summary>
	/// <param name="options">The options for this context.</param>
	/// <param name="request">The accessor to get current request information.</param>
	protected DataContextWithBus(DbContextOptions<TContext> options, IRequestContextAccessor request)
		: base(options)
	{
		_request = request;
		ChangeTracker.DetectedEntityChanges += OnDetectedEntityChanges;
	}

	/// <inheritdoc/>
	protected override bool AutoSetEntryValues => true;

	protected override void SetEntryValues(IEnumerable<EntityEntry> entries)
	{
		if (!AutoSetEntryValues)
		{
			return;
		}

		foreach (var entry in entries)
		{
			switch (entry.State)
			{
				case EntityState.Unchanged:
				case EntityState.Modified when _unchangedEntities.Contains(entry.CurrentValues["Id"]):
					continue;
			}

			if (entry.Entity is not IAuditable auditable)
			{
				continue;
			}

			var user = GetCurrentUser();

			if (string.IsNullOrWhiteSpace(user))
			{
				return;
			}

			var dateTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
			switch (entry.State)
			{
				case EntityState.Added:
					auditable.CreatedBy = user;
					auditable.UpdatedBy = user;
					auditable.CreatedAt = dateTime;
					auditable.UpdatedAt = dateTime;

					break;
				case EntityState.Deleted:
					entry.State = EntityState.Modified;
					auditable.DeletedBy = user;
					auditable.DeletedAt = dateTime;
					auditable.IsDeleted = true;

					break;
				case EntityState.Modified:
					auditable.UpdatedBy = user;
					auditable.UpdatedAt = dateTime;
					break;
			}
		}
	}

	/// <summary>
	/// Gets the current user from the request context.
	/// </summary>
	/// <returns></returns>
	private string GetCurrentUser()
	{
		var principal = _request?.Context?.User;

		if (principal == null)
		{
			return null;
		}

		return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? principal.Identity?.Name;
	}

	protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
	{
		base.ConfigureConventions(configurationBuilder);
		configurationBuilder.Properties<DateTime>()
		                    .HaveConversion<UniversalTimeConverter>();
		configurationBuilder.Properties<DateTime?>()
		                    .HaveConversion<UniversalTimeConverter>();
	}

	public override void Dispose()
	{
		ChangeTracker.DetectedEntityChanges -= OnDetectedEntityChanges;
		base.Dispose();
	}

	public override ValueTask DisposeAsync()
	{
		ChangeTracker.DetectedEntityChanges -= OnDetectedEntityChanges;
		return base.DisposeAsync();
	}

	protected virtual void OnDetectedEntityChanges(object sender, DetectedEntityChangesEventArgs args)
	{
		Debug.WriteLine(args.Entry.DebugView.LongView);

		if (args.Entry.State != EntityState.Modified || args.ChangesFound)
		{
			return;
		}

		var id = args.Entry.CurrentValues["Id"]; //ExpressionHelper.GetPropertyValue(args.Entry.CurrentValues["Id"]);
		_unchangedEntities.Add(id);
	}
}