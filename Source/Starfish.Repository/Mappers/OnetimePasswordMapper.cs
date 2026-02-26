using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Mappers;

[DbContext(typeof(IdentityDataContext))]
internal class OnetimePasswordMapper : IEntityTypeConfiguration<OnetimePassword>
{
	private const string TABLE_NAME = "onetime_password";
	private const string COLUMN_REQUEST_ID = "request_id";
	private const string COLUMN_CODE = "code";
	private const string COLUMN_RECIPIENT = "recipient";
	private const string COLUMN_EXPIRATION = "expiration";
	private const string COLUMN_CHECKED = "checked";
	private const string COLUMN_DURATION = "duration";
	private const string COLUMN_USAGE = "usage";

	public void Configure(EntityTypeBuilder<OnetimePassword> builder)
	{
		builder.ToTable(TABLE_NAME);

		builder.SnowflakeId();

		builder.HasIndex(x => x.RequestId)
		       .HasDatabaseName($"{TABLE_NAME}_idx_{COLUMN_REQUEST_ID}");

		builder.SnowflakeId();

		builder.Property(x => x.RequestId)
		       .HasColumnName(COLUMN_REQUEST_ID)
		       .HasMaxLength(OnetimePasswordConstants.RequestIdLength)
		       .IsRequired();

		builder.Property(x => x.Code)
		       .HasColumnName(COLUMN_CODE)
		       .HasMaxLength(OnetimePasswordConstants.CodeLength)
		       .IsRequired();

		builder.Property(x => x.Recipient)
		       .HasColumnName(COLUMN_RECIPIENT)
		       .HasMaxLength(OnetimePasswordConstants.RecipientMaxLength)
		       .IsRequired();

		builder.Property(x => x.Expiration)
		       .HasColumnName(COLUMN_EXPIRATION);

		builder.Property(x => x.Checked)
		       .HasColumnName(COLUMN_CHECKED);

		builder.Property(x => x.Duration)
		       .HasColumnName(COLUMN_DURATION);

		builder.Property(x => x.Usage)
		       .HasColumnName(COLUMN_USAGE);

		builder.CreatedAtUtc();
	}
}