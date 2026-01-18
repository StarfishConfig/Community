using System.Diagnostics;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Nerosoft.Euonia.Modularity;
using Nerosoft.Euonia.Repository;
using Nerosoft.Euonia.Repository.EfCore;

namespace Nerosoft.Starfish.Repository;

/// <summary>
/// Base data context with bus and request context support.
/// </summary>
internal abstract class DataContextBase<TContext> : Euonia.Repository.EfCore.DataContextBase<TContext>
	where TContext : DbContext, IRepositoryContext
{
	private readonly List<object> _unchangedEntities = [];
	private readonly IRequestContextAccessor _request;
	private readonly ILoggerFactory _logger;

	/// <summary>
	/// Initializes a new instance of the <see cref="DataContextBase{TContext}"/> class.
	/// </summary>
	/// <param name="options">The options for this context.</param>
	/// <param name="request">The accessor to get current request information.</param>
	/// <param name="logger">The logger factory.</param>
	protected DataContextBase(DbContextOptions<TContext> options, IRequestContextAccessor request, ILoggerFactory logger)
		: base(options)
	{
		_request = request;
		_logger = logger;
		Logger = logger.CreateLogger<TContext>();
		// ReSharper disable once VirtualMemberCallInConstructor
		ChangeTracker.DetectedEntityChanges += OnDetectedEntityChanges;
	}

	protected ILogger<TContext> Logger { get; }

	/// <inheritdoc/>
	protected override bool AutoSetEntryValues => true;

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseLoggerFactory(_logger);
		optionsBuilder.LogTo(Console.WriteLine);
		base.OnConfiguring(optionsBuilder);
	}

	public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
	{
		SetEntryValues(ChangeTracker.Entries());
		return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
	}

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

			var user = GetCurrentUser();

			if (string.IsNullOrWhiteSpace(user))
			{
				return;
			}

			var dateTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
			switch (entry.State)
			{
				case EntityState.Added:
					entry.CurrentValues[nameof(IAuditable.CreatedBy)] = user;
					entry.CurrentValues[nameof(IAuditable.CreatedAt)] = dateTime;
					entry.CurrentValues[nameof(IAuditable.UpdatedBy)] = user;
					entry.CurrentValues[nameof(IAuditable.UpdatedAt)] = dateTime;

					break;
				case EntityState.Deleted:
					if (entry.Entity is IAuditable)
					{
						entry.State = EntityState.Modified;
						entry.CurrentValues[nameof(IAuditable.DeletedBy)] = user;
						entry.CurrentValues[nameof(IAuditable.DeletedAt)] = dateTime;
						entry.CurrentValues[nameof(IAuditable.IsDeleted)] = true;
					}

					break;
				case EntityState.Modified:
					entry.CurrentValues[nameof(IAuditable.UpdatedBy)] = user;
					entry.CurrentValues[nameof(IAuditable.UpdatedAt)] = dateTime;
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