using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Mappers;

[DbContext(typeof(ProjectDataContext))]
internal class ConfigurationMapper : IEntityTypeConfiguration<Configuration>
{
	private const string TABLE_NAME = "configuration";
	private const string COLUMN_TEAM_ID = "team_id";
	private const string COLUMN_FORK_ID = "fork_id";
	private const string COLUMN_CODE = "code";
	private const string COLUMN_NAME = "name";
	private const string COLUMN_TAGS = "tags";
	private const string COLUMN_DESCRIPTION = "description";
	private const string COLUMN_STATUS = "status";
	private const string COLUMN_SHARED = "shared";

	public void Configure(EntityTypeBuilder<Configuration> builder)
	{
		builder.ToTable(TABLE_NAME);

		builder.SnowflakeId();

		builder.HasIndex(e => e.TeamId)
		       .HasDatabaseName($"{TABLE_NAME}_idx_{COLUMN_TEAM_ID}");

		builder.HasIndex(e => e.Code)
		       .HasDatabaseName($"{TABLE_NAME}_idx_{COLUMN_CODE}");

		builder.HasIndex(e => e.ForkId)
		       .HasDatabaseName($"{TABLE_NAME}_idx_{COLUMN_FORK_ID}");

		builder.Property(e => e.TeamId)
		       .HasColumnName(COLUMN_TEAM_ID)
		       .IsRequired();

		builder.Property(e => e.ForkId)
		       .HasColumnName(COLUMN_FORK_ID);

		builder.Property(t => t.Tags)
		       .HasColumnName(COLUMN_TAGS)
		       .HasMaxLength(LengthConstraints.ConfigurationTagsMaximumLength)
		       .IsUnicode();

		builder.Property(e => e.Code)
		       .HasColumnName(COLUMN_CODE)
		       .IsRequired()
		       .HasMaxLength(LengthConstraints.ConfigurationCodeMaximumLength);

		builder.Property(e => e.Name)
		       .HasColumnName(COLUMN_NAME)
		       .IsRequired()
		       .HasMaxLength(LengthConstraints.ConfigurationNameMaximumLength)
		       .IsUnicode();

		builder.Property(e => e.Description)
		       .HasColumnName(COLUMN_DESCRIPTION)
		       .HasMaxLength(LengthConstraints.DescriptionMaximumLength)
		       .IsUnicode();

		builder.Property(e => e.Status)
		       .HasColumnName(COLUMN_STATUS)
		       .IsRequired()
		       .HasDefaultValue(ConfigurationStatus.None);

		builder.Property(e => e.Shared)
		       .HasColumnName(COLUMN_SHARED)
		       .IsRequired()
		       .HasDefaultValue(false);

		builder.ConfigureAuditableProperties();

		builder.HasMany(e => e.Permissions)
		       .WithOne(e => e.Configuration)
		       .HasForeignKey(e => e.ConfigurationId)
		       .OnDelete(DeleteBehavior.Cascade);

		builder.HasMany(e => e.Items)
		       .WithOne(t => t.Configuration)
		       .HasForeignKey(t => t.ConfigurationId)
		       .OnDelete(DeleteBehavior.Cascade);

		builder.HasMany(e => e.References)
		       .WithOne(t => t.Configuration)
		       .HasForeignKey(t => t.ConfigurationId);
	}
}