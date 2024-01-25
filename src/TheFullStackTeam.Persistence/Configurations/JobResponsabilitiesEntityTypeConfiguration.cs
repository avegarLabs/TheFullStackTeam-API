using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class JobResponsabilitiesEntityTypeConfiguration : IEntityTypeConfiguration<JobResponsabilities>
{
    public void Configure(EntityTypeBuilder<JobResponsabilities> builder)
    {
        builder.ToTable("JobResponsabilities");

        builder.Property(p => p.ResposabilityDescription);

        builder.HasOne(o => o.Jobs)
            .WithMany(co => co.JobResponsabilities)
            .HasForeignKey(fk => fk.JobId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
