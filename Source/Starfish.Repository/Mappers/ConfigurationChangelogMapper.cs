using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Mappers;

[DbContext(typeof(ConfigsDataContext))]
internal class ConfigurationChangelogMapper : IEntityTypeConfiguration<ConfigurationChangelog>
{
	public void Configure(EntityTypeBuilder<ConfigurationChangelog> builder)
	{
		builder.ToTable("configuration_changelog");

		builder.SnowflakeId();

		builder.HasIndex(e => e.ConfigurationId)
		       .HasDatabaseName("configuration_changelog_idx_fk_cfg_id");

		builder.HasIndex(e => e.ItemId)
		       .HasDatabaseName("configuration_changelog_idx_fk_item_id");

		builder.Property(e => e.ConfigurationId)
		       .IsRequired();

		builder.Property(e => e.ItemId)
		       .HasColumnName("item_id")
		       .IsRequired();

		builder.Property(e => e.Key)
		       .HasColumnName("key")
		       .IsRequired()
		       .HasMaxLength(200);

		builder.Property(t => t.Value)
		       .HasColumnName("value")
		       .HasMaxLength(2048);

		builder.Property(e => e.ChangedBy)
		       .HasColumnName("changed_by")
		       .IsRequired()
		       .HasMaxLength(64);

		builder.Property(e => e.ChangeType)
		       .HasColumnName("change_type")
		       .IsRequired()
		       .HasMaxLength(20);

		builder.Property(e => e.ChangedAt)
		       .HasColumnName("changed_at");
	}
}