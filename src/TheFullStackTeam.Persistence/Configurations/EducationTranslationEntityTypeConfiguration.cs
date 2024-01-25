using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class EducationTranslationEntityTypeConfiguration : IEntityTypeConfiguration<EducationTranslation>
{
    public void Configure(EntityTypeBuilder<EducationTranslation> builder)
    {
        builder.ToTable("EducationTranslations");

        builder.Property(p => p.Program).IsRequired(false);
        builder.Property(p => p.FieldsOfStudy).IsRequired(false);

        builder.HasKey(pk => new { pk.EducationId, pk.LanguageId });
    }
}
