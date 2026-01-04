using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nerosoft.Starfish.Persist.Entities;

namespace Nerosoft.Starfish.Persist.Configurations;

[DbContext(typeof(IdentityDataContext))]
internal sealed class UserRoleEntityConfiguration : IEntityTypeConfiguration<UserRoleEntity>
{
	public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
	{
		builder.ToTable("user_role");
		builder.HasKey(x => x.Id);
		
	}
}