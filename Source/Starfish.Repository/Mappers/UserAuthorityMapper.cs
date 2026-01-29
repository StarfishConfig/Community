using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Mappers;

[DbContext(typeof(IdentityDataContext))]
internal class UserAuthorityMapper : IEntityTypeConfiguration<UserAuthority>
{
	public void Configure(EntityTypeBuilder<UserAuthority> builder)
	{
		builder.ToTable("user_authority");
		builder.HasKey(x => x.Id);

		builder.HasIndex(x => x.UserId)
		       .HasDatabaseName("user_authority_idx_fk");

		builder.HasIndex(x => new { x.Provider, x.OpenId })
		       .HasDatabaseName("user_authority_idx_unique")
		       .IsUnique();

		builder.SnowflakeId();

		builder.Property(x => x.UserId)
		       .HasColumnName("user_id")
		       .HasMaxLength(64)
		       .IsRequired();

		builder.Property(x => x.Provider)
		       .HasColumnName("provider")
		       .HasMaxLength(100)
		       .IsRequired();

		builder.Property(x => x.OpenId)
		       .HasColumnName("open_id")
		       .HasMaxLength(200)
		       .IsRequired();

		builder.Property(x => x.Name)
		       .HasColumnName("name")
		       .HasMaxLength(255)
		       .IsRequired(false);

		builder.CreatedAtUtc();
	}
}