using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Mappers;

[DbContext(typeof(ProjectDataContext))]
internal class ConfigurationPermissionMapper : IEntityTypeConfiguration<ConfigurationPermission>
{
	public void Configure(EntityTypeBuilder<ConfigurationPermission> builder)
	{
		builder.ToTable("configuration_permission");

		builder.HasIndex(t => t.ConfigurationId)
			   .HasDatabaseName("configuration_permission_idx_cfg_id");

		builder.HasIndex(t => t.UserId)
			   .HasDatabaseName("configuration_permission_idx_user_id");

		builder.SnowflakeId();

		builder.Property(t => t.ConfigurationId)
			.HasColumnName("configuration_id");

		builder.Property(t => t.UserId)
			.HasColumnName("user_id")
			.HasMaxLength(64);

		builder.Property(t => t.Read)
			.HasColumnName("read")
			.HasDefaultValue(true);

		builder.Property(t => t.Write)
			.HasColumnName("write")
			.HasDefaultValue(false);

		builder.Property(t => t.Publish)
			.HasColumnName("publish")
			.HasDefaultValue(false);

		builder.HasOne(t => t.Configuration)
			   .WithMany()
			   .HasForeignKey(t => t.ConfigurationId)
			   .OnDelete(DeleteBehavior.Cascade);
	}
}