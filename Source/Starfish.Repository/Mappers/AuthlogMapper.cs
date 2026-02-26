using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;
using Nerosoft.Starfish.Shared;

namespace Nerosoft.Starfish.Repository.Mappers;

[DbContext(typeof(IdentityDataContext))]
internal sealed class AuthlogMapper : IEntityTypeConfiguration<Authlog>
{
	private const string TABLE_NAME = "authlog";
	private const string COLUMN_USER_ID = "user_id";
	private const string COLUMN_USERNAME = "username";
	private const string COLUMN_GRANT_TYPE = "grant_type";
	private const string COLUMN_REQUEST_ID = "request_id";
	private const string COLUMN_IP_ADDRESS = "ip_address";
	private const string COLUMN_USER_AGENT = "user_agent";
	private const string COLUMN_REFERER = "referer";
	private const string COLUMN_APP_NAME = "app_name";
	private const string COLUMN_APP_VERSION = "app_version";
	private const string COLUMN_OS_PLATFORM = "os_platform";
	private const string COLUMN_SOURCE = "source";
	private const string COLUMN_SUCCESS = "success";
	private const string COLUMN_TIMESTAMP = "timestamp";
	private const string COLUMN_REMARK = "remark";

	public void Configure(EntityTypeBuilder<Authlog> builder)
	{
		builder.ToTable(TABLE_NAME);
		builder.HasKey(x => x.Id);

		builder.HasIndex(x => x.Username)
		       .HasDatabaseName($"{TABLE_NAME}_idx_{COLUMN_USERNAME}");

		builder.HasIndex(x => x.Timestamp)
		       .HasDatabaseName($"{TABLE_NAME}_idx_{COLUMN_TIMESTAMP}");

		builder.HasIndex(t => t.Success)
		       .HasDatabaseName($"{TABLE_NAME}_idx_{COLUMN_SUCCESS}");

		builder.SnowflakeId();

		builder.Property(x => x.UserId)
		       .HasColumnName(COLUMN_USER_ID)
		       .HasMaxLength(LengthConstraints.UserIdMaximumLength)
		       .IsRequired();

		builder.Property(x => x.Username)
		       .HasColumnName(COLUMN_USERNAME)
		       .HasMaxLength(LengthConstraints.UsernameMaximumLength);

		builder.Property(x => x.GrantType)
		       .HasColumnName(COLUMN_GRANT_TYPE)
		       .HasMaxLength(32)
		       .IsRequired();

		builder.Property(t => t.RequestId)
		       .HasColumnName(COLUMN_REQUEST_ID)
		       .HasMaxLength(LengthConstraints.RequestIdLength);

		builder.Property(x => x.IpAddress)
		       .HasColumnName(COLUMN_IP_ADDRESS)
		       .HasMaxLength(LengthConstraints.IpAddressMaximumLength);

		builder.Property(x => x.UserAgent)
		       .HasColumnName(COLUMN_USER_AGENT)
		       .HasMaxLength(512);

		builder.Property(x => x.Referer)
		       .HasColumnName(COLUMN_REFERER)
		       .HasMaxLength(255);

		builder.Property(x => x.AppName)
		       .HasColumnName(COLUMN_APP_NAME)
		       .HasMaxLength(32);

		builder.Property(x => x.AppVersion)
		       .HasColumnName(COLUMN_APP_VERSION)
		       .HasMaxLength(20);

		builder.Property(x => x.OsPlatform)
		       .HasColumnName(COLUMN_OS_PLATFORM)
		       .HasMaxLength(16);

		builder.Property(x => x.Source)
		       .HasColumnName(COLUMN_SOURCE)
		       .HasMaxLength(32);

		builder.Property(x => x.Success)
		       .HasColumnName(COLUMN_SUCCESS)
		       .IsRequired();

		builder.Property(x => x.Timestamp)
		       .HasColumnName(COLUMN_TIMESTAMP)
		       .IsRequired();

		builder.Property(x => x.Remark)
		       .HasColumnName(COLUMN_REMARK)
		       .HasMaxLength(LengthConstraints.RemarkMaximumLength);
	}
}