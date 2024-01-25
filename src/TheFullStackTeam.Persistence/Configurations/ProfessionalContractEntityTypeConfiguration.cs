using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class ProfessionalContractEntityTypeConfiguration : IEntityTypeConfiguration<ProfessionalContractType>
{
    public void Configure(EntityTypeBuilder<ProfessionalContractType> builder)
    {
        builder.ToTable("ProfessionalContratType");

        builder.HasOne(o => o.Professional)
             .WithMany(co => co.ProfessionalContractTypes)
             .HasForeignKey(fk => fk.ProfessionalId)
             .OnDelete(DeleteBehavior.Cascade);
    }
}
