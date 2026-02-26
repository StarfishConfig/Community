using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Mappers;

[DbContext(typeof(ProjectDataContext))]
internal class ConfigurationItemMapper : IEntityTypeConfiguration<ConfigurationItem>
{
	private const string TABLE_NAME = "configuration_item";
	private const string COLUMN_CONFIGURATION_ID = "configuration_id";
	private const string COLUMN_KEY = "key";
	private const string COLUMN_VALUE = "value";

	public void Configure(EntityTypeBuilder<ConfigurationItem> builder)
	{
		builder.ToTable(TABLE_NAME);

		builder.SnowflakeId();

		builder.HasIndex(e => e.ConfigurationId)
		       .HasDatabaseName($"{TABLE_NAME}_idx_cfg_id");

		builder.HasIndex(e => e.Key)
		       .HasDatabaseName($"{TABLE_NAME}_idx_{COLUMN_KEY}");

		builder.Property(e => e.ConfigurationId)
		       .HasColumnName(COLUMN_CONFIGURATION_ID)
		       .IsRequired();

		builder.Property(e => e.Key)
		       .HasColumnName(COLUMN_KEY)
		       .IsRequired()
		       .HasMaxLength(LengthConstraints.ConfigurationKeyMaximumLength)
		       .IsUnicode();

		builder.Property(e => e.Value)
		       .HasColumnName(COLUMN_VALUE)
		       .IsRequired()
		       .HasMaxLength(LengthConstraints.ConfigurationValueMaximumLength)
		       .IsUnicode();

		builder.ConfigureAuditableProperties();

		builder.HasOne(t => t.Configuration)
		       .WithMany(t => t.Items)
		       .HasForeignKey(t => t.ConfigurationId)
		       .OnDelete(DeleteBehavior.Cascade);
	}
}