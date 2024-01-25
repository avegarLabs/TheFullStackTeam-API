using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class UserRolesEntityTypeConfiguration : IEntityTypeConfiguration<UserRoles>
{
    public void Configure(EntityTypeBuilder<UserRoles> builder)
    {
        builder.ToTable("UserRoles");

        builder.Property(p => p.RoleName);


        builder.HasOne(o => o.User)
            .WithMany(co => co.UserRoles)
            .HasForeignKey(fk => fk.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.Roles)
            .WithMany(co => co.UserRoles)
            .HasForeignKey(fk => fk.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
