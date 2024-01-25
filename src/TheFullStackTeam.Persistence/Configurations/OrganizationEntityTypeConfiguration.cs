using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class OrganizationEntityTypeConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.ToTable("Organizations");

        builder.Property(p => p.Name).HasMaxLength(Organization.NameMaxLenght);
        builder.Property(p => p.Description).HasMaxLength(Organization.DescriptionMaxLenght);
        builder.Property(p => p.Phone).HasMaxLength(Professional.PhoneMaxLenght);
        builder.Property(p => p.ContactEmail).HasMaxLength(Professional.ContactEmailMaxLenght);
        builder.Property(p => p.OrganizationWeb);
        builder.Property(p => p.LinkedInProfile);
        builder.Property(p => p.YoutubeProfile);
        builder.Property(p => p.Zise);
        builder.Property(p => p.Sector);

        //Value object config
        builder.OwnsOne(a => a.Logo).WithOwner();
        builder.OwnsOne(a => a.Address).WithOwner();

        builder.HasOne(o => o.Country)
         .WithMany(co => co.Organizations)
         .HasForeignKey(fk => fk.CountryId)
         .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.User)
           .WithMany(co => co.Organizations)
           .HasForeignKey(fk => fk.UserId)
           .OnDelete(DeleteBehavior.Cascade);
    }
}
