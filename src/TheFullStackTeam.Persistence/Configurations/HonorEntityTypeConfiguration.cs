using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

/// <summary>
/// Honor entity type configuration
/// </summary>
public class HonorEntityTypeConfiguration : IEntityTypeConfiguration<Honor>
{
    public void Configure(EntityTypeBuilder<Honor> builder)
    {
        builder.ToTable("Honors");
        builder.HasKey(x => x.Id);

        builder.Property(p => p.Title).HasMaxLength(Honor.TitleMaxLenght);
        builder.Property(p => p.Description).HasMaxLength(Honor.DescriptionMaxLenght);

        builder.HasOne(p => p.Professional).WithMany(m => m.Honors).HasForeignKey(p => p.ProfessionalId);
        builder.HasOne(p => p.Organization).WithMany(m => m.Honors).HasForeignKey(p => p.OrganizationId);
    }
}