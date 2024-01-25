using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class ProfessionalLanguegeEntityTypeConfiguration : IEntityTypeConfiguration<ProfessionalLanguage>
{
    public void Configure(EntityTypeBuilder<ProfessionalLanguage> builder)
    {
        builder.ToTable("ProfessionalLanguage");
        builder.Property(p => p.LanguegeName);


        builder.HasOne(o => o.Professional)
            .WithMany(co => co.ProfessionalLanguages)
            .HasForeignKey(fk => fk.ProfessionalId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.Language)
            .WithMany(co => co.ProfessionalLanguages)
            .HasForeignKey(fk => fk.LanguegeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
