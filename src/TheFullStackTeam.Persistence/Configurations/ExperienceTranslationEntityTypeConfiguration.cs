using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class ExperienceTranslationEntityTypeConfiguration : IEntityTypeConfiguration<ExperienceTranslation>
{
    public void Configure(EntityTypeBuilder<ExperienceTranslation> builder)
    {
        builder.ToTable("ExperienceTranslations");

        builder.HasKey(pk => new { pk.ExperienceId, pk.LanguageId });
    }
}
