using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Mappers;

[DbContext(typeof(ProjectDataContext))]
internal class ConfigurationItemMapper : IEntityTypeConfiguration<ConfigurationItem>
{
	public void Configure(EntityTypeBuilder<ConfigurationItem> builder)
	{
		builder.ToTable(ConfigurationConstants.TableName.ConfigurationItem);

		builder.SnowflakeId();

		builder.HasIndex(e => e.ConfigurationId)
		       .HasDatabaseName($"{ConfigurationConstants.TableName.ConfigurationItem}_{ConfigurationConstants.IndexName.ConfigurationId}");

		builder.Property(e => e.ConfigurationId)
		       .HasColumnName(ConfigurationConstants.ColumnName.ConfigurationId)
		       .IsRequired();

		builder.Property(e => e.Key)
		       .HasColumnName(ConfigurationConstants.ColumnName.Key)
		       .IsRequired()
		       .HasMaxLength(ConfigurationConstants.StringLength.KeyMaximumLength)
		       .IsUnicode();

		builder.Property(e => e.Value)
		       .HasColumnName(ConfigurationConstants.ColumnName.Value)
		       .IsRequired()
		       .HasMaxLength(ConfigurationConstants.StringLength.ValueMaximumLength)
		       .IsUnicode();

		builder.ConfigureAuditableProperties();

		builder.HasOne(t => t.Configuration)
		       .WithMany(t => t.Items)
		       .HasForeignKey(t => t.ConfigurationId)
		       .OnDelete(DeleteBehavior.Cascade);
	}
}