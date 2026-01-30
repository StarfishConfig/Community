using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Mappers;

[DbContext(typeof(ConfigsDataContext))]
internal class ConfigurationForkMapper : IEntityTypeConfiguration<ConfigurationFork>
{
	public void Configure(EntityTypeBuilder<ConfigurationFork> builder)
	{
		builder.ToTable("configuration_fork");

		builder.HasIndex(t => t.ConfigurationId)
		       .HasDatabaseName("configuration_fork_idx_cfg_id");
		builder.HasIndex(t => t.ItemId)
		       .HasDatabaseName("configuration_fork_idx_item_id");

		builder.SnowflakeId();

		builder.Property(t => t.Tags)
		       .IsRequired();

		builder.Property(t => t.Value)
		       .IsRequired();

		builder.ConfigureAuditableProperties();
	}
}