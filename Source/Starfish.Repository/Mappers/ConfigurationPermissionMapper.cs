using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Mappers;

[DbContext(typeof(ProjectDataContext))]
internal class ConfigurationPermissionMapper : IEntityTypeConfiguration<ConfigurationPermission>
{
	public void Configure(EntityTypeBuilder<ConfigurationPermission> builder)
	{
		builder.ToTable(ConfigurationConstants.TableName.ConfigurationPermission);

		builder.HasIndex(t => t.ConfigurationId)
		       .HasDatabaseName($"{ConfigurationConstants.TableName.ConfigurationPermission}_{ConfigurationConstants.IndexName.ConfigurationId}");

		builder.HasIndex(t => t.UserId)
		       .HasDatabaseName($"{ConfigurationConstants.TableName.ConfigurationPermission}_{ConfigurationConstants.IndexName.UserId}");

		builder.SnowflakeId();

		builder.Property(t => t.ConfigurationId)
		       .HasColumnName(ConfigurationConstants.ColumnName.ConfigurationId);

		builder.Property(t => t.UserId)
		       .HasColumnName(ConfigurationConstants.ColumnName.UserId)
		       .HasMaxLength(64);

		builder.Property(t => t.Read)
		       .HasColumnName(ConfigurationConstants.ColumnName.Read)
		       .HasDefaultValue(true);

		builder.Property(t => t.Write)
		       .HasColumnName(ConfigurationConstants.ColumnName.Write)
		       .HasDefaultValue(false);

		builder.Property(t => t.Publish)
		       .HasColumnName(ConfigurationConstants.ColumnName.Publish)
		       .HasDefaultValue(false);

		builder.HasOne(t => t.Configuration)
		       .WithMany(t => t.Permissions)
		       .HasForeignKey(t => t.ConfigurationId)
		       .OnDelete(DeleteBehavior.Cascade);
	}
}