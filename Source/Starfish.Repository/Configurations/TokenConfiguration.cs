using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Configurations;

[DbContext(typeof(IdentityDataContext))]
internal class TokenConfiguration : IEntityTypeConfiguration<Token>
{
	public void Configure(EntityTypeBuilder<Token> builder)
	{
		builder.ToTable("token");
		builder.HasKey(x => x.Id);

		builder.HasIndex(x => new { x.Type, x.Key })
		       .IsUnique()
		       .HasDatabaseName("token_idx_unique");

		builder.SnowflakeId();

		builder.Property(x => x.Type)
		       .HasColumnName("type")
		       .HasMaxLength(32)
		       .IsRequired();

		builder.Property(x => x.Key)
		       .HasColumnName("key")
		       .HasMaxLength(256)
		       .IsRequired();

		builder.Property(x => x.Subject)
		       .HasColumnName("subject")
		       .IsRequired();

		builder.Property(x => x.Issues)
		       .HasColumnName("issues");

		builder.Property(x => x.Expires)
		       .HasColumnName("expires");
	}
}