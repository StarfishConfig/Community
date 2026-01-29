using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Mappers;

[DbContext(typeof(IdentityDataContext))]
internal sealed class UserRoleMapper : IEntityTypeConfiguration<UserRole>
{
	public void Configure(EntityTypeBuilder<UserRole> builder)
	{
		builder.ToTable("user_role");
		builder.HasKey(x => x.Id);

		builder.HasIndex(x => new { x.UserId, x.Name })
		       .IsUnique()
		       .HasDatabaseName("user_role_idx_unique");

		builder.ShortUniqueId();

		builder.Property(x => x.UserId)
		       .HasColumnName("user_id")
		       .IsRequired();

		builder.Property(x => x.Name)
		       .HasColumnName("name")
		       .HasMaxLength(100)
		       .IsRequired();

		builder.CreatedAtUtc();
	}
}