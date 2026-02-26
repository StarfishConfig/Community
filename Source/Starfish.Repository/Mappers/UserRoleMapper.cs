using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Mappers;

[DbContext(typeof(IdentityDataContext))]
internal sealed class UserRoleMapper : IEntityTypeConfiguration<UserRole>
{
	private const string TABLE_NAME = "user_role";
	private const string COLUMN_USER_ID = "user_id";
	private const string COLUMN_NAME = "name";

	public void Configure(EntityTypeBuilder<UserRole> builder)
	{
		builder.ToTable(TABLE_NAME);
		builder.HasKey(x => x.Id);

		builder.HasIndex(x => new { x.UserId, x.Name })
		       .IsUnique()
		       .HasDatabaseName($"{TABLE_NAME}_idx_unique");

		builder.ShortUniqueId();

		builder.Property(x => x.UserId)
		       .HasColumnName(COLUMN_USER_ID)
		       .HasMaxLength(LengthConstraints.UserIdMaximumLength)
		       .IsRequired();

		builder.Property(x => x.Name)
		       .HasColumnName(COLUMN_NAME)
		       .HasMaxLength(100)
		       .IsRequired();

		builder.CreatedAtUtc();
	}
}