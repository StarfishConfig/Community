using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Mappers;

[DbContext(typeof(IdentityDataContext))]
internal class TeamMemberMapper : IEntityTypeConfiguration<TeamMember>
{
	private const string TABLE_NAME = "team_member";
	private const string COLUMN_TEAM_ID = "team_id";
	private const string COLUMN_USER_ID = "user_id";

	public void Configure(EntityTypeBuilder<TeamMember> builder)
	{
		builder.ToTable(TABLE_NAME);

		builder.HasIndex(t => new { t.TeamId, t.UserId })
		       .HasDatabaseName($"{TABLE_NAME}_idx_unique")
		       .IsUnique();

		builder.SnowflakeId();

		builder.Property(tm => tm.TeamId)
		       .HasColumnName(COLUMN_TEAM_ID)
		       .IsRequired();

		builder.Property(tm => tm.UserId)
		       .HasColumnName(COLUMN_USER_ID)
		       .HasMaxLength(LengthConstraints.UserIdMaximumLength)
		       .IsRequired();

		builder.CreatedAtUtc();
	}
}