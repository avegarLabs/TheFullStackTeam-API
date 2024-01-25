using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class RolesEntityTypeConfiguration : IEntityTypeConfiguration<Roles>
{
    public void Configure(EntityTypeBuilder<Roles> builder)
    {
        builder.ToTable("Roles");

        builder.Property(p => p.Name).HasMaxLength(Roles.NameMaxLenght);
        builder.HasIndex(p => p.Moniker).IsUnique();
    }
}
