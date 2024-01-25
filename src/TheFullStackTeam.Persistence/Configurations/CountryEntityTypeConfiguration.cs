using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

/// <summary>
/// Country entity type configuration
/// </summary>
public class CountryEntityTypeConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("Countries");

        builder.Property(p => p.Cca2).HasMaxLength(Country.Cca2MaxLenght).IsRequired(false);
        builder.Property(p => p.Cca3).HasMaxLength(Country.Cca3MaxLenght).IsRequired(false);
        builder.Property(p => p.Ccn3).HasMaxLength(Country.Ccn3MaxLenght).IsRequired(false);
        builder.Property(p => p.Tld).HasMaxLength(Country.TldMaxLenght).IsRequired(false);
        builder.Property(p => p.NativeName).HasMaxLength(Country.NativeNameMaxLenght);
        builder.Property(p => p.CommonName).HasMaxLength(Country.CommonNameMaxLenght);
        builder.Property(p => p.OfficialName).HasMaxLength(Country.OfficialNameMaxLenght);
    }
}