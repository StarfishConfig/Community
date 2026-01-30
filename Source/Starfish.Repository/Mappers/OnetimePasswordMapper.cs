using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Mappers;

[DbContext(typeof(IdentityDataContext))]
internal class OnetimePasswordMapper : IEntityTypeConfiguration<OnetimePassword>
{
	public void Configure(EntityTypeBuilder<OnetimePassword> builder)
	{
		builder.ToTable(OnetimePasswordConstants.TableName);

		builder.SnowflakeId();

		builder.HasIndex(x => x.RequestId)
			   .HasDatabaseName(OnetimePasswordConstants.RequestIndexName);

		builder.SnowflakeId();

		builder.Property(x => x.RequestId)
			   .HasColumnName(OnetimePasswordConstants.RequestIdColumnName)
			   .HasMaxLength(OnetimePasswordConstants.RequestIdLength)
			   .IsRequired();

		builder.Property(x => x.Code)
			   .HasColumnName(OnetimePasswordConstants.CodeColumnName)
			   .HasMaxLength(OnetimePasswordConstants.CodeLength)
			   .IsRequired();

		builder.Property(x => x.Recipient)
			   .HasColumnName(OnetimePasswordConstants.RecipientColumnName)
			   .HasMaxLength(OnetimePasswordConstants.RecipientMaxLength)
			   .IsRequired();

		builder.Property(x => x.Expiration)
			   .HasColumnName(OnetimePasswordConstants.ExpirationColumnName);

		builder.Property(x => x.Checked)
			   .HasColumnName(OnetimePasswordConstants.CheckedColumnName);

		builder.Property(x => x.Duration)
			   .HasColumnName(OnetimePasswordConstants.DurationColumnName);

		builder.Property(x => x.Usage)
			   .HasColumnName(OnetimePasswordConstants.UsageColumnName);

		builder.CreatedAtUtc();
	}
}
