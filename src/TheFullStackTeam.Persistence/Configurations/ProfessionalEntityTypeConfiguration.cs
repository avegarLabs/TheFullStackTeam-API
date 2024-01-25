using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class ProfessionalEntityTypeConfiguration : IEntityTypeConfiguration<Professional>
{
    public void Configure(EntityTypeBuilder<Professional> builder)
    {
        builder.ToTable("Professionals");
        builder.Property(p => p.Name).HasMaxLength(Professional.NameMaxLenght);
        builder.Property(p => p.AboutMe).HasMaxLength(Professional.AboutMeMaxLenght);
        builder.Property(p => p.Title).HasMaxLength(Professional.HeadLineMaxLenght);
        builder.Property(p => p.Phone).HasMaxLength(Professional.PhoneMaxLenght);
        builder.Property(p => p.ContactEmail).HasMaxLength(Professional.ContactEmailMaxLenght);
        builder.Property(p => p.PersonalWeb);
        builder.Property(p => p.LinkedInProfile);
        builder.Property(p => p.YoutubeProfile);
        builder.HasIndex(p => p.Moniker).IsUnique();
        builder.Property(p => p.VitaePath);
        builder.Property(p => p.VitaeId);

        builder.OwnsOne(a => a.Picture).WithOwner();
        builder.OwnsOne(a => a.Availability).WithOwner();

        builder.HasOne(o => o.Country)
         .WithMany(co => co.Professionals)
         .HasForeignKey(fk => fk.CountryId)
         .OnDelete(DeleteBehavior.Cascade);
    }
}
