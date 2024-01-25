using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

/// <summary>
/// Certification entity type configuration
/// </summary>
public class CertificationEntityTypeConfiguration : IEntityTypeConfiguration<Certification>
{
    public void Configure(EntityTypeBuilder<Certification> builder)
    {
        builder.Property(p => p.Authority).HasMaxLength(Certification.AuthorityMaxLenght);
        builder.Property(p => p.LicenseNumber).HasMaxLength(Certification.LicenceNumberMaxLenght);
        builder.Property(p => p.Url).HasMaxLength(Certification.UrlMaxLenght).IsRequired(false);
    }
}
