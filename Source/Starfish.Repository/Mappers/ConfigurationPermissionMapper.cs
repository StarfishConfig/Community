using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Mappers;

[DbContext(typeof(ProjectDataContext))]
internal class ConfigurationPermissionMapper : IEntityTypeConfiguration<ConfigurationPermission>
{
	private const string TABLE_NAME = "configuration_permission";
	private const string COLUMN_CONFIGURATION_ID = "configuration_id";
	private const string COLUMN_USER_ID = "user_id";
	private const string COLUMN_READ = "read";
	private const string COLUMN_WRITE = "write";
	private const string COLUMN_PUBLISH = "publish";

	public void Configure(EntityTypeBuilder<ConfigurationPermission> builder)
	{
		builder.ToTable(TABLE_NAME);

		builder.HasIndex(t => t.ConfigurationId)
		       .HasDatabaseName($"{TABLE_NAME}_idx_cfg_id");

		builder.HasIndex(t => t.UserId)
		       .HasDatabaseName($"{TABLE_NAME}_idx_{COLUMN_USER_ID}");

		builder.SnowflakeId();

		builder.Property(t => t.ConfigurationId)
		       .HasColumnName(COLUMN_CONFIGURATION_ID);

		builder.Property(t => t.UserId)
		       .HasColumnName(COLUMN_USER_ID)
		       .HasMaxLength(64);

		builder.Property(t => t.Read)
		       .HasColumnName(COLUMN_READ)
		       .HasDefaultValue(true);

		builder.Property(t => t.Write)
		       .HasColumnName(COLUMN_WRITE)
		       .HasDefaultValue(false);

		builder.Property(t => t.Publish)
		       .HasColumnName(COLUMN_PUBLISH)
		       .HasDefaultValue(false);

		builder.HasOne(t => t.Configuration)
		       .WithMany(t => t.Permissions)
		       .HasForeignKey(t => t.ConfigurationId)
		       .OnDelete(DeleteBehavior.Cascade);
	}
}