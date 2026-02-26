using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Mappers;

[DbContext(typeof(ProjectDataContext))]
internal class ConfigurationChangelogMapper : IEntityTypeConfiguration<ConfigurationChangelog>
{
	private const string TABLE_NAME = "configuration_changelog";
	private const string COLUMN_CONFIGURATION_ID = "configuration_id";
	private const string COLUMN_TEAM_ID = "team_id";
	private const string COLUMN_KEY = "key";
	private const string COLUMN_VALUE = "value";
	private const string COLUMN_CHANGE_TYPE = "change_type";
	private const string COLUMN_CHANGED_BY = "changed_by";
	private const string COLUMN_CHANGED_AT = "changed_at";

	public void Configure(EntityTypeBuilder<ConfigurationChangelog> builder)
	{
		builder.ToTable(TABLE_NAME);

		builder.SnowflakeId();

		builder.HasIndex(e => e.ConfigurationId)
		       .HasDatabaseName($"{TABLE_NAME}_idx_cfg_id");

		builder.HasIndex(e => e.ItemId)
		       .HasDatabaseName($"{TABLE_NAME}_idx_{COLUMN_TEAM_ID}");

		builder.Property(e => e.ConfigurationId)
		       .HasColumnName(COLUMN_CONFIGURATION_ID)
		       .IsRequired();

		builder.Property(e => e.ItemId)
		       .HasColumnName(COLUMN_TEAM_ID)
		       .IsRequired();

		builder.Property(e => e.Key)
		       .HasColumnName(COLUMN_KEY)
		       .IsRequired()
		       .HasMaxLength(LengthConstraints.ConfigurationKeyMaximumLength);

		builder.Property(t => t.Value)
		       .HasColumnName(COLUMN_VALUE)
		       .HasMaxLength(LengthConstraints.ConfigurationValueMaximumLength);

		builder.Property(e => e.ChangedBy)
		       .HasColumnName(COLUMN_CHANGED_BY)
		       .IsRequired()
		       .HasMaxLength(LengthConstraints.UserIdMaximumLength);

		builder.Property(e => e.ChangeType)
		       .HasColumnName(COLUMN_CHANGE_TYPE)
		       .IsRequired()
		       .HasMaxLength(20);

		builder.Property(e => e.ChangedAt)
		       .HasColumnName(COLUMN_CHANGED_AT);
	}
}