using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Mappers;

[DbContext(typeof(IdentityDataContext))]
internal class UserAuthorityMapper : IEntityTypeConfiguration<UserAuthority>
{
	private const string TABLE_NAME = "user_authority";
	private const string COLUMN_USER_ID = "user_id";
	private const string COLUMN_PROVIDER = "provider";
	private const string COLUMN_OPEN_ID = "open_id";
	private const string COLUMN_NAME = "name";

	public void Configure(EntityTypeBuilder<UserAuthority> builder)
	{
		builder.ToTable(TABLE_NAME);
		builder.HasKey(x => x.Id);

		builder.HasIndex(x => x.UserId)
		       .HasDatabaseName($"{TABLE_NAME}_idx_{COLUMN_USER_ID}");

		builder.HasIndex(x => new { x.Provider, x.OpenId })
		       .HasDatabaseName($"{TABLE_NAME}_idx_unique")
		       .IsUnique();

		builder.SnowflakeId();

		builder.Property(x => x.UserId)
		       .HasColumnName(COLUMN_USER_ID)
		       .HasMaxLength(LengthConstraints.UserIdMaximumLength)
		       .IsRequired();

		builder.Property(x => x.Provider)
		       .HasColumnName(COLUMN_PROVIDER)
		       .HasMaxLength(100)
		       .IsRequired();

		builder.Property(x => x.OpenId)
		       .HasColumnName(COLUMN_OPEN_ID)
		       .HasMaxLength(200)
		       .IsRequired();

		builder.Property(x => x.Name)
		       .HasColumnName(COLUMN_NAME)
		       .HasMaxLength(255)
		       .IsRequired(false);

		builder.CreatedAtUtc();
	}
}