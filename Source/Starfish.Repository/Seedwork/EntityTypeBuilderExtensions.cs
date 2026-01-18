using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Euonia.Repository;
using Nerosoft.Euonia.Repository.EfCore;

namespace Nerosoft.Starfish.Repository;

internal static class EntityTypeBuilderExtensions
{
	private const string COLUMN_CREATED_AT = "created_at";
	private const string COLUMN_CREATED_BY = "created_by";
	private const string COLUMN_UPDATED_AT = "updated_at";
	private const string COLUMN_UPDATED_BY = "updated_by";
	private const string COLUMN_IS_DELETED = "is_deleted";
	private const string COLUMN_DELETED_AT = "deleted_at";
	private const string COLUMN_DELETED_BY = "deleted_by";

	/// <summary>
	/// Configure auditable properties for entity type <typeparamref name="TEntity"/>.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <param name="builder"></param>
	public static void ConfigureAuditableProperties<TEntity>(this EntityTypeBuilder<TEntity> builder)
		where TEntity : class, IAuditable
	{
		builder.CreatedAtUtc();

		builder.Property(t => t.CreatedBy)
		       .HasColumnName(COLUMN_CREATED_BY)
		       .HasMaxLength(64)
		       .IsRequired();

		builder.UpdatedAtUtc();

		builder.Property(t => t.UpdatedBy)
		       .HasColumnName(COLUMN_UPDATED_BY)
		       .HasMaxLength(64)
		       .IsRequired();

		builder.Property(t => t.IsDeleted)
		       .HasColumnName(COLUMN_IS_DELETED)
		       .HasDefaultValue(false)
		       .IsRequired();

		builder.DeletedAtUtc();

		builder.Property(t => t.DeletedBy)
		       .HasColumnName(COLUMN_DELETED_BY)
		       .HasMaxLength(64);
	}

	/// <summary>
	/// Configure tombstone properties for entity type <typeparamref name="TEntity"/>.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <param name="builder"></param>
	public static void ConfigureTombstoneProperty<TEntity>(this EntityTypeBuilder<TEntity> builder)
		where TEntity : class, ITombstone
	{
		builder.Property(t => t.IsDeleted)
		       .HasColumnName(COLUMN_IS_DELETED)
		       .HasDefaultValue(false)
		       .IsRequired();
	}

	/// <summary>
	/// Configure Snowflake ID property for entity type <typeparamref name="TEntity"/>.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <param name="builder">The <see cref="EntityTypeBuilder{TEntity}"/> to configure.</param>
	/// <param name="columnName">Specifies the column name for the Snowflake ID.</param>
	public static void SnowflakeId<TEntity>(this EntityTypeBuilder<TEntity> builder, string columnName = "id")
		where TEntity : class, IEntity<long>
	{
		builder.HasKey(t => t.Id);
		builder.Property(t => t.Id)
		       .HasColumnName(columnName)
		       .IsRequired()
		       .HasValueGenerator<SnowflakeIdValueGenerator>()
		       .ValueGeneratedOnAdd();
	}

	/// <summary>
	/// Configure Short Unique ID property for entity type <typeparamref name="TEntity"/>.
	/// </summary>
	/// <param name="builder">The <see cref="EntityTypeBuilder{TEntity}"/> to configure.</param>
	/// <param name="columnName">Specifies the column name for the Short Unique ID.</param>
	/// <typeparam name="TEntity"></typeparam>
	public static void ShortUniqueId<TEntity>(this EntityTypeBuilder<TEntity> builder, string columnName = "id")
		where TEntity : class, IEntity<string>
	{
		builder.HasKey(t => t.Id);
		builder.Property(t => t.Id)
		       .HasColumnName(columnName)
		       .HasMaxLength(20)
		       .IsRequired()
		       .HasValueGenerator<ShortUniqueIdValueGenerator>()
		       .ValueGeneratedOnAdd();
	}

	/// <summary>
	/// Configure CreatedAt property with UTC time for entity type <typeparamref name="TEntity"/>.
	/// </summary>
	/// <param name="builder">The <see cref="EntityTypeBuilder{TEntity}"/> to configure.</param>
	/// <typeparam name="TEntity"></typeparam>
	public static void CreatedAtUtc<TEntity>(this EntityTypeBuilder<TEntity> builder)
		where TEntity : class, IHasCreateTime
	{
		builder.Property(t => t.CreatedAt)
		       .HasColumnName(COLUMN_CREATED_AT)
		       .HasValueGenerator<UtcTimeValueGenerator>()
		       .IsRequired();
	}

	/// <summary>
	/// Configure UpdatedAt property with UTC time for entity type <typeparamref name="TEntity"/>.
	/// </summary>
	/// <param name="builder">The <see cref="EntityTypeBuilder{TEntity}"/> to configure.</param>
	/// <typeparam name="TEntity"></typeparam>
	public static void UpdatedAtUtc<TEntity>(this EntityTypeBuilder<TEntity> builder)
		where TEntity : class, IHasUpdateTime
	{
		builder.Property(t => t.UpdatedAt)
		       .HasColumnName(COLUMN_UPDATED_AT)
		       .HasValueGenerator<UtcTimeValueGenerator>()
		       .IsRequired();
	}

	/// <summary>
	/// Configure DeletedAt property with UTC time for entity type <typeparamref name="TEntity"/>.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <param name="builder">The <see cref="EntityTypeBuilder{TEntity}"/> to configure.</param>
	public static void DeletedAtUtc<TEntity>(this EntityTypeBuilder<TEntity> builder)
		where TEntity : class, IHasDeleteTime
	{
		builder.Property(t => t.DeletedAt)
		       .HasColumnName(COLUMN_DELETED_AT);
	}

	/// <summary>
	/// Configure tombstone index for entity type <typeparamref name="TEntity"/>.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <param name="builder">The <see cref="EntityTypeBuilder{TEntity}"/> to configure.</param>
	/// <param name="tableName"></param>
	public static void HasTombstoneIndex<TEntity>(this EntityTypeBuilder<TEntity> builder, string tableName = null)
		where TEntity : class, ITombstone
	{
		builder.HasIndex(t => t.IsDeleted)
		       .HasDatabaseName($"{tableName ?? typeof(TEntity).Name.ToLower()}_idx_tombstone");
	}
}