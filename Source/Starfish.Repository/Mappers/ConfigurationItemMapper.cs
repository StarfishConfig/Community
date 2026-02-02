using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Mappers;

[DbContext(typeof(ConfigsDataContext))]
internal class ConfigurationItemMapper : IEntityTypeConfiguration<ConfigurationItem>
{
	public void Configure(EntityTypeBuilder<ConfigurationItem> builder)
	{
		builder.ToTable("configuration_item");

		builder.SnowflakeId();

		builder.HasIndex(e => e.ConfigurationId)
		       .HasDatabaseName("configuration_item_idx_cfg_id");

		builder.HasIndex(e => e.EnvironmentId)
		       .HasDatabaseName("configuration_item_idx_env_id");

		builder.Property(e => e.ConfigurationId)
		       .HasColumnName("configuration_id")
		       .IsRequired();

		builder.Property(e => e.EnvironmentId)
		       .HasColumnName("environment_id")
		       .IsRequired();

		builder.Property(e => e.ForkId)
		       .HasColumnName("fork_id");

		builder.Property(e => e.Key)
		       .HasColumnName("key")
		       .IsRequired()
		       .HasMaxLength(200)
		       .IsUnicode();

		builder.Property(e => e.Value)
		       .HasColumnName("value")
		       .IsRequired()
		       .HasMaxLength(4000)
		       .IsUnicode();

		builder.Property(e => e.Tags)
		       .HasColumnName("tags")
		       .HasMaxLength(1000);

		builder.Property(e => e.Status)
		       .HasColumnName("status")
		       .IsRequired();
	}
}