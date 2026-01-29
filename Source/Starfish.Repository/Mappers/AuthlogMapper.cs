using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Mappers;

[DbContext(typeof(IdentityDataContext))]
internal sealed class AuthlogMapper : IEntityTypeConfiguration<Authlog>
{
	public void Configure(EntityTypeBuilder<Authlog> builder)
	{
		builder.ToTable("authlog");
		builder.HasKey(x => x.Id);

		builder.HasIndex(x => x.Username)
		       .HasDatabaseName("authlog_idx_username");

		builder.HasIndex(x => x.Timestamp)
		       .HasDatabaseName("authlog_idx_timestamp");

		builder.HasIndex(t => t.Success)
		       .HasDatabaseName("authlog_idx_success");

		builder.SnowflakeId();

		builder.Property(x => x.UserId)
		       .HasColumnName("user_id")
		       .HasMaxLength(255)
		       .IsRequired();

		builder.Property(x => x.Username)
		       .HasColumnName("username")
		       .HasMaxLength(255);

		builder.Property(x => x.GrantType)
		       .HasColumnName("grant_type")
		       .HasMaxLength(32)
		       .IsRequired();

		builder.Property(t => t.RequestId)
		       .HasColumnName("request_id")
		       .HasMaxLength(32);

		builder.Property(x => x.IpAddress)
		       .HasColumnName("ip_address")
		       .HasMaxLength(15);

		builder.Property(x => x.UserAgent)
		       .HasColumnName("user_agent")
		       .HasMaxLength(512);

		builder.Property(x => x.Referer)
		       .HasColumnName("referer")
		       .HasMaxLength(255);

		builder.Property(x => x.AppName)
		       .HasColumnName("app_name")
		       .HasMaxLength(32);

		builder.Property(x => x.AppVersion)
		       .HasColumnName("app_version")
		       .HasMaxLength(20);

		builder.Property(x => x.OsPlatform)
		       .HasColumnName("os_platform")
		       .HasMaxLength(16);

		builder.Property(x => x.Source)
		       .HasColumnName("source")
		       .HasMaxLength(32);

		builder.Property(x => x.Success)
		       .HasColumnName("success")
		       .IsRequired();

		builder.Property(x => x.Timestamp)
		       .HasColumnName("timestamp")
		       .IsRequired();

		builder.Property(x => x.Remark)
		       .HasColumnName("remark")
		       .HasMaxLength(1024);
	}
}