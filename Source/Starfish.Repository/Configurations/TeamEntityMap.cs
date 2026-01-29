using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Configurations;

[DbContext(typeof(IdentityDataContext))]
internal class TeamEntityMap : IEntityTypeConfiguration<Team>
{
	public void Configure(EntityTypeBuilder<Team> builder)
	{
		builder.ToTable(TeamConstants.TableName);

		builder.HasIndex(t => t.OwnerId).HasDatabaseName(TeamConstants.OwnerIdIndexName);

		builder.SnowflakeId();

		builder.Property(t => t.OwnerId)
		       .HasColumnName(TeamConstants.OwnerIdColumnName)
		       .HasMaxLength(TeamConstants.OwnerIdLength)
		       .IsRequired();

		builder.Property(t => t.Name)
		       .HasColumnName(TeamConstants.NameColumnName)
		       .HasMaxLength(TeamConstants.NameMaximumLength)
		       .IsRequired()
		       .IsUnicode();

		builder.Property(t => t.Description)
		       .HasColumnName(TeamConstants.DescriptionColumnName)
		       .HasMaxLength(TeamConstants.DescriptionMaximumLength)
		       .IsUnicode();

		builder.Property(t => t.MembersCount)
		       .HasColumnName(TeamConstants.MembersCountColumnName)
		       .IsRequired()
		       .HasDefaultValue(0);

		builder.ConfigureAuditableProperties();

		builder.HasMany(t => t.Members)
		       .WithOne()
		       .HasForeignKey(t => t.TeamId)
		       .OnDelete(DeleteBehavior.Cascade);
	}
}