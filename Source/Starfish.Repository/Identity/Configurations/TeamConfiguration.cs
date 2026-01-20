using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Configurations;

[DbContext(typeof(IdentityDataContext))]
internal class TeamConfiguration : IEntityTypeConfiguration<Team>
{
	public void Configure(EntityTypeBuilder<Team> builder)
	{
		builder.ToTable("team");

		builder.HasIndex(t => t.OwnerId).HasDatabaseName("idx_team_owner_id");

		builder.SnowflakeId();

		builder.Property(t => t.OwnerId)
		       .HasColumnName("owner_id")
		       .HasMaxLength(64)
		       .IsRequired();

		builder.Property(t => t.Name)
		       .HasColumnName("name")
		       .HasMaxLength(200)
		       .IsRequired()
		       .IsUnicode();

		builder.Property(t => t.Description)
		       .HasColumnName("description")
		       .HasMaxLength(500)
		       .IsUnicode();

		builder.Property(t => t.MemberCount)
		       .HasColumnName("member_count")
		       .IsRequired()
		       .HasDefaultValue(0);

		builder.ConfigureAuditableProperties();

		builder.HasMany(t => t.Members)
		       .WithOne()
		       .HasForeignKey(t => t.TeamId)
		       .OnDelete(DeleteBehavior.Cascade);
	}
}