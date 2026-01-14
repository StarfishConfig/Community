using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Configurations;

[DbContext(typeof(IdentityDataContext))]
internal sealed class AuthlogConfiguration : IEntityTypeConfiguration<Authlog>
{
	public void Configure(EntityTypeBuilder<Authlog> builder)
	{
		builder.ToTable("authlog");
		builder.HasKey(x => x.Id);

		builder.HasIndex(x => x.UserId).HasDatabaseName("authlog_idx_userid");

		builder.ShortUniqueId();

		builder.Property(x => x.UserId)
		       .HasColumnName("user_id")
		       .IsRequired();

		builder.Property(x => x.Username)
		       .HasColumnName("username")
		       .HasMaxLength(255);

		builder.Property(x => x.GrantType)
		       .HasColumnName("grant_type")
		       .HasMaxLength(64)
		       .IsRequired();

		builder.Property(t => t.RequestId)
		       .HasColumnName("request_id")
		       .HasMaxLength(64);

		builder.Property(x => x.IpAddress)
		       .HasColumnName("ip_address")
		       .HasMaxLength(45);

		builder.Property(x => x.UserAgent)
		       .HasColumnName("user_agent")
		       .HasMaxLength(512);

		builder.Property(x => x.Referer)
		       .HasColumnName("referer")
		       .HasMaxLength(512);

		builder.Property(x => x.AppName)
		       .HasColumnName("app_name")
		       .HasMaxLength(32);

		builder.Property(x => x.AppVersion)
		       .HasColumnName("app_version")
		       .HasMaxLength(20);

		builder.Property(x => x.OsPlatform)
		       .HasColumnName("os_platform")
		       .HasMaxLength(64);

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