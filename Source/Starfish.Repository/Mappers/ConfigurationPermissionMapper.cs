using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Mappers;

[DbContext(typeof(ConfigsDataContext))]
internal class ConfigurationPermissionMapper : IEntityTypeConfiguration<ConfigurationPermission>
{
	public void Configure(EntityTypeBuilder<ConfigurationPermission> builder)
	{
		builder.ToTable("configuration_permission");

		builder.HasIndex(t => t.ConfigurationId)
		       .HasDatabaseName("configuration_permission_idx_cfg_id");
		builder.HasIndex(t => t.EnvironmentId)
		       .HasDatabaseName("configuration_permission_idx_env_id");
		builder.HasIndex(t => t.UserId)
		       .HasDatabaseName("configuration_permission_idx_user_id");

		builder.SnowflakeId();
	}
}