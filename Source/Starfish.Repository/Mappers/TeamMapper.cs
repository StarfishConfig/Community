using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Mappers;

[DbContext(typeof(IdentityDataContext))]
internal class TeamMapper : IEntityTypeConfiguration<Team>
{
	private const string TABLE_NAME = "team";
	private const string COLUMN_OWNER_ID = "owner_id";
	private const string COLUMN_NAME = "name";
	private const string COLUMN_DESCRIPTION = "description";
	private const string COLUMN_MEMBERS_COUNT = "members_count";
	
	public void Configure(EntityTypeBuilder<Team> builder)
	{
		builder.ToTable(TABLE_NAME);

		builder.HasIndex(t => t.OwnerId)
		       .HasDatabaseName($"{TABLE_NAME}_idx_{COLUMN_OWNER_ID}");

		builder.SnowflakeId();

		builder.Property(t => t.OwnerId)
		       .HasColumnName(COLUMN_OWNER_ID)
		       .HasMaxLength(TeamConstants.OwnerIdLength)
		       .IsRequired();

		builder.Property(t => t.Name)
		       .HasColumnName(COLUMN_NAME)
		       .HasMaxLength(TeamConstants.NameMaximumLength)
		       .IsRequired()
		       .IsUnicode();

		builder.Property(t => t.Description)
		       .HasColumnName(COLUMN_DESCRIPTION)
		       .HasMaxLength(TeamConstants.DescriptionMaximumLength)
		       .IsUnicode();

		builder.Property(t => t.MembersCount)
		       .HasColumnName(COLUMN_MEMBERS_COUNT)
		       .IsRequired()
		       .HasDefaultValue(0);

		builder.ConfigureAuditableProperties();

		builder.HasMany(t => t.Members)
		       .WithOne()
		       .HasForeignKey(t => t.TeamId)
		       .OnDelete(DeleteBehavior.Cascade);
	}
}