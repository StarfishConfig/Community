using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Mappers;

[DbContext(typeof(IdentityDataContext))]
internal class TeamMemberMapper : IEntityTypeConfiguration<TeamMember>
{
	public void Configure(EntityTypeBuilder<TeamMember> builder)
	{
		builder.ToTable("team_member");

		builder.HasIndex(t => new { t.TeamId, t.UserId })
		       .HasDatabaseName("idx_team_member_unique")
		       .IsUnique();

		builder.SnowflakeId();

		builder.Property(tm => tm.TeamId)
		       .HasColumnName("team_id")
		       .IsRequired();

		builder.Property(tm => tm.UserId)
		       .HasColumnName("user_id")
		       .HasMaxLength(64)
		       .IsRequired();

		builder.CreatedAtUtc();
	}
}