using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Repository.Entities;

namespace Nerosoft.Starfish.Repository.Configurations;

[DbContext(typeof(SystemDataContext))]
internal class MessageTemplateConfiguration : IEntityTypeConfiguration<MessageTemplate>
{
	public void Configure(EntityTypeBuilder<MessageTemplate> builder)
	{
		builder.ToTable("message_template");

		builder.HasIndex(x => new { x.Code, x.Language, x.Type })
		       .HasDatabaseName("idx_message_template_unique");

		builder.SnowflakeId();

		builder.Property(t => t.Name)
		       .HasColumnName("name")
		       .HasMaxLength(200)
		       .IsRequired()
		       .IsUnicode();

		builder.Property(t => t.Code)
		       .HasColumnName("code")
		       .HasMaxLength(100)
		       .IsRequired();

		builder.Property(t => t.Language)
		       .HasColumnName("language")
		       .HasMaxLength(16)
		       .IsRequired();

		builder.Property(t => t.Type)
		       .HasColumnName("type")
		       .IsRequired();

		builder.Property(t => t.Subject)
		       .HasColumnName("subject")
		       .HasMaxLength(500)
		       .IsRequired()
		       .IsUnicode();

		builder.Property(t => t.Body)
		       .HasColumnName("body")
		       .HasMaxLength(int.MaxValue)
		       .IsRequired()
		       .IsUnicode();
		
		builder.Property(t => t.Default)
		       .HasColumnName("default")
		       .HasDefaultValue(false);

		builder.ConfigureAuditableProperties();
	}
}