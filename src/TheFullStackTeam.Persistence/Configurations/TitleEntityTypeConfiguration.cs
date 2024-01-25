using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

/// <summary>
/// Title entity type configuration
/// </summary>
public class TitleEntityTypeConfiguration : IEntityTypeConfiguration<Title>
{
    public void Configure(EntityTypeBuilder<Title> builder)
    {
        builder.ToTable("Titles");

        builder.HasKey(pk => pk.Id);

        builder.HasDiscriminator(d => d.TitleType)
            .HasValue<Title>(nameof(Title))
            .HasValue<Certification>(nameof(Certification))
            .HasValue<Course>(nameof(Course))
            .HasValue<Education>(nameof(Education));

        builder.HasOne(o => o.Professional)
            .WithMany(co => co.Titles)
            .HasForeignKey(fk => fk.ProfessionalId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.Organization)
       .WithMany(co => co.Titles)
       .HasForeignKey(fk => fk.OrganizationId)
       .OnDelete(DeleteBehavior.Cascade);
    }
}