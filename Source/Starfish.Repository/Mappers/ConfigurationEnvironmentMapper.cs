using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Mappers;

[DbContext(typeof(ProjectDataContext))]
internal sealed class ConfigurationEnvironmentMapper : IEntityTypeConfiguration<ConfigurationEnvironment>
{
	public void Configure(EntityTypeBuilder<ConfigurationEnvironment> builder)
	{
		builder.ToTable("configuration_environment");

		builder.HasIndex(t => new { t.ConfigurationId, t.Name })
		       .HasDatabaseName("configuration_environment_idx_unique")
		       .IsUnique();

		builder.SnowflakeId();

		builder.Property(t => t.Name)
		       .HasColumnName("name")
		       .HasMaxLength(32);

		builder.Property(t => t.Secret)
		       .HasColumnName("secret")
		       .HasMaxLength(64);

		builder.HasOne(t => t.Configuration)
		       .WithMany(c => c.Environments)
		       .HasForeignKey(t => t.ConfigurationId);
	}
}