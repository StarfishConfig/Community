using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Mappers;

[DbContext(typeof(ProjectDataContext))]
internal class ConfigurationMapper : IEntityTypeConfiguration<Configuration>
{
	public void Configure(EntityTypeBuilder<Configuration> builder)
	{
		builder.ToTable(ConfigurationConstants.TableName.Configuration);

		builder.SnowflakeId();

		builder.HasIndex(e => e.TeamId)
		       .HasDatabaseName($"{ConfigurationConstants.TableName.Configuration}_{ConfigurationConstants.IndexName.TeamId}");

		builder.Property(e => e.TeamId)
		       .HasColumnName(ConfigurationConstants.ColumnName.TeamId)
		       .IsRequired();

		builder.Property(e => e.Code)
		       .HasColumnName(ConfigurationConstants.ColumnName.Code)
		       .IsRequired()
		       .HasMaxLength(ConfigurationConstants.StringLength.CodeMaximumLength);

		builder.Property(e => e.Name)
		       .HasColumnName(ConfigurationConstants.ColumnName.Name)
		       .IsRequired()
		       .HasMaxLength(ConfigurationConstants.StringLength.NameMaximumLength)
		       .IsUnicode();

		builder.Property(e => e.Description)
		       .HasColumnName(ConfigurationConstants.ColumnName.Description)
		       .HasMaxLength(ConfigurationConstants.StringLength.DescriptionMaximumLength)
		       .IsUnicode();

		builder.Property(e => e.Status)
		       .HasColumnName(ConfigurationConstants.ColumnName.Status)
		       .IsRequired()
		       .HasDefaultValue(ConfigurationStatus.None);

		builder.Property(e => e.Shared)
		       .HasColumnName(ConfigurationConstants.ColumnName.Shared)
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
	}
}