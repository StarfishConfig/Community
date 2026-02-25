using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Mappers;

[DbContext(typeof(ProjectDataContext))]
internal class ConfigurationReferenceMapper : IEntityTypeConfiguration<ConfigurationReference>
{
	public void Configure(EntityTypeBuilder<ConfigurationReference> builder)
	{
		builder.ToTable(ConfigurationConstants.TableName.ConfigurationReference);

		builder.HasIndex(t => new { t.ConfigurationId, t.ReferenceId })
		       .HasDatabaseName($"{ConfigurationConstants.TableName.ConfigurationReference}_{ConfigurationConstants.IndexName.Unique}")
		       .IsUnique();

		builder.SnowflakeId();

		builder.Property(t => t.ConfigurationId)
		       .HasColumnName(ConfigurationConstants.ColumnName.ConfigurationId);

		builder.Property(t => t.ReferenceId)
		       .HasColumnName(ConfigurationConstants.ColumnName.ReferenceId);

		builder.HasOne(t => t.Configuration)
		       .WithMany(t => t.References)
		       .HasForeignKey(t => t.ConfigurationId)
		       .OnDelete(DeleteBehavior.Cascade);
	}
}