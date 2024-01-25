using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class ProfessionalTranslationEntityTypeConfiguration : IEntityTypeConfiguration<ProfessionalTranslation>
{
    public void Configure(EntityTypeBuilder<ProfessionalTranslation> builder)
    {
        builder.ToTable("ProfessionalTranslations");

        builder.HasKey(pk => new { pk.ProfessionalId, pk.LanguageId });
    }
}
