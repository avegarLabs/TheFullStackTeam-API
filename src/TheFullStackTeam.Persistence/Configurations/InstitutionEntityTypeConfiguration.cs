using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class InstitutionEntityTypeConfiguration : IEntityTypeConfiguration<Institution>
{
    public void Configure(EntityTypeBuilder<Institution> builder)
    {
        builder.ToTable("Institution");
        builder.Property(p => p.Name).HasMaxLength(Organization.NameMaxLenght);
        builder.Property(p => p.Description).HasMaxLength(Organization.DescriptionMaxLenght);
        builder.Property(p => p.City);
        builder.HasIndex(ix => ix.Moniker).IsUnique();

        //Value object config
        builder.OwnsOne(a => a.Logo).WithOwner();


        builder.HasOne(o => o.Country)
        .WithMany(co => co.Institutions)
        .HasForeignKey(fk => fk.CountryId)
        .OnDelete(DeleteBehavior.Restrict);
    }
}
