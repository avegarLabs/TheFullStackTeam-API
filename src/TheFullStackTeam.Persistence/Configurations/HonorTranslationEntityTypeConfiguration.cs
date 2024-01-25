using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class HonorTranslationEntityTypeConfiguration : IEntityTypeConfiguration<HonorTranslation>
{
    public void Configure(EntityTypeBuilder<HonorTranslation> builder)
    {
        builder.ToTable("HonorTranslations");

        builder.HasKey(pk => new { pk.HonorId, pk.LanguageId });
    }
}
