using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Mappers;

/// <summary>
/// Configures the entity of type <see cref="User"/>.
/// </summary>
[DbContext(typeof(IdentityDataContext))]
internal class UserMapper : IEntityTypeConfiguration<User>
{
	private const string TABLE_NAME = "user";
	private const string COLUMN_USERNAME = "username";
	private const string COLUMN_PASSWORD_HASH = "password_hash";
	private const string COLUMN_PASSWORD_SALT = "password_salt";
	private const string COLUMN_NICKNAME = "nickname";
	private const string COLUMN_EMAIL = "email";
	private const string COLUMN_PHONE = "phone";
	private const string COLUMN_ACCESS_FAILED_COUNT = "access_failed_count";
	private const string COLUMN_LOCKOUT_END = "lockout_end";
	private const string COLUMN_PASSWORD_CHANGED_AT = "password_changed_at";

	/// <summary>
	/// Configures the entity of type <see cref="User"/>.
	/// </summary>
	/// <param name="builder"></param>
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ToTable(TABLE_NAME);
		builder.HasKey(x => x.Id);

		builder.HasIndex(x => x.Username).HasDatabaseName($"{TABLE_NAME}_idx_{COLUMN_USERNAME}");
		builder.HasIndex(x => x.Email).HasDatabaseName($"{TABLE_NAME}_idx_{COLUMN_EMAIL}");
		builder.HasIndex(x => x.Phone).HasDatabaseName($"{TABLE_NAME}_idx_{COLUMN_PHONE}");
		builder.HasTombstoneIndex();

		builder.ShortUniqueId();

		builder.Property(x => x.Username)
		       .HasColumnName(COLUMN_USERNAME)
		       .HasMaxLength(LengthConstraints.UsernameMaximumLength)
		       .IsRequired()
		       .IsUnicode();

		builder.Property(x => x.PasswordHash)
		       .HasMaxLength(512)
		       .HasColumnName(COLUMN_PASSWORD_HASH);

		builder.Property(x => x.PasswordSalt)
		       .HasMaxLength(32)
		       .HasColumnName(COLUMN_PASSWORD_SALT);

		builder.Property(x => x.Nickname)
		       .HasColumnName(COLUMN_NICKNAME)
		       .HasMaxLength(LengthConstraints.NicknameMaximumLength)
		       .IsUnicode();

		builder.Property(x => x.Email)
		       .HasColumnName(COLUMN_EMAIL)
		       .HasMaxLength(LengthConstraints.EmailMaximumLength);

		builder.Property(x => x.Phone)
		       .HasColumnName(COLUMN_PHONE)
		       .HasMaxLength(LengthConstraints.PhoneMaximumLength);

		builder.Property(x => x.AccessFailedCount)
		       .HasColumnName(COLUMN_ACCESS_FAILED_COUNT)
		       .HasDefaultValue(0);

		builder.Property(x => x.LockoutEnd)
		       .HasColumnName(COLUMN_LOCKOUT_END);

		builder.Property(x => x.PasswordChangedAt)
		       .HasColumnName(COLUMN_PASSWORD_CHANGED_AT);

		builder.CreatedAtUtc();

		builder.UpdatedAtUtc();

		builder.DeletedAtUtc();

		builder.ConfigureTombstoneProperty();

		builder.Property(t => t.DeletedAt)
		       .HasColumnName("deleted_at");

		builder.HasMany(x => x.Roles)
		       .WithOne(x => x.User)
		       .HasForeignKey(x => x.UserId)
		       .OnDelete(DeleteBehavior.Cascade);

		builder.HasMany(x => x.Authorities)
		       .WithOne(x => x.User)
		       .HasForeignKey(a => a.UserId)
		       .OnDelete(DeleteBehavior.Cascade);
	}
}