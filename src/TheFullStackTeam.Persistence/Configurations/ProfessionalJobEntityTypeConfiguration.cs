using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class ProfessionalJobEntityTypeConfiguration : IEntityTypeConfiguration<ProfessionalJobType>
{
    public void Configure(EntityTypeBuilder<ProfessionalJobType> builder)
    {
        builder.ToTable("ProfessionalJobType");

        builder.HasOne(o => o.Professional)
            .WithMany(co => co.ProfessionalJobTypes)
            .HasForeignKey(fk => fk.ProfessionalId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
