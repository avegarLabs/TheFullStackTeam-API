using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class LanguageEntityTypeConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.ToTable("Languages");

        builder.Property(p => p.Name).HasMaxLength(Language.NameMaxLenght);
        builder.Property(p => p.LocalName).HasMaxLength(Language.LocalNameMaxLenght);
        builder.Property(p => p.IsoCode).HasMaxLength(Language.IsoCodeMaxLenght);
        builder.Property(p => p.ThreeLetterIsoCode).HasMaxLength(Language.ThreeLetterIsoCodeMaxLenght);
    }
}

