using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Mappers;

[DbContext(typeof(ProjectDataContext))]
internal class ConfigurationMapper : IEntityTypeConfiguration<Configuration>
{
	public void Configure(EntityTypeBuilder<Configuration> builder)
	{
		builder.ToTable("configuration");

		builder.SnowflakeId();

		builder.HasIndex(e => e.TeamId)
		       .HasDatabaseName("configuration_idx_team_id");

		builder.Property(e => e.TeamId)
		       .IsRequired();

		builder.Property(e => e.Code)
		       .HasColumnName("code")
		       .IsRequired()
		       .HasMaxLength(100);

		builder.Property(e => e.Name)
		       .HasColumnName("name")
		       .IsRequired()
		       .HasMaxLength(200)
		       .IsUnicode();

		builder.Property(e => e.Description)
		       .HasColumnName("description")
		       .HasMaxLength(1000)
		       .IsUnicode();

		builder.ConfigureAuditableProperties();

		builder.HasMany(e => e.Environments)
		       .WithOne(e => e.Configuration)
		       .HasForeignKey(e => e.ConfigurationId)
		       .OnDelete(DeleteBehavior.Cascade);

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