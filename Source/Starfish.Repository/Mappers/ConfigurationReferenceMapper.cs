using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Mappers;

[DbContext(typeof(ProjectDataContext))]
internal class ConfigurationReferenceMapper : IEntityTypeConfiguration<ConfigurationReference>
{
	private const string TABLE_NAME = "configuration_reference";
	private const string COLUMN_CONFIGURATION_ID = "configuration_id";
	private const string COLUMN_REFERENCE_ID = "reference_id";

	public void Configure(EntityTypeBuilder<ConfigurationReference> builder)
	{
		builder.ToTable(TABLE_NAME);

		builder.HasIndex(t => new { t.ConfigurationId, t.ReferenceId })
		       .HasDatabaseName($"{TABLE_NAME}_idx_unique")
		       .IsUnique();

		builder.SnowflakeId();

		builder.Property(t => t.ConfigurationId)
		       .HasColumnName(COLUMN_CONFIGURATION_ID);

		builder.Property(t => t.ReferenceId)
		       .HasColumnName(COLUMN_REFERENCE_ID);

		builder.HasOne(t => t.Configuration)
		       .WithMany(t => t.References)
		       .HasForeignKey(t => t.ConfigurationId)
		       .OnDelete(DeleteBehavior.Cascade);
	}
}