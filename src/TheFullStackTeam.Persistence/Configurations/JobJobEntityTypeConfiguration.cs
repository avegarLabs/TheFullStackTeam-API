using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class JobJobEntityTypeConfiguration : IEntityTypeConfiguration<JobsJobType>
{
    public void Configure(EntityTypeBuilder<JobsJobType> builder)
    {
        builder.ToTable("JobJobType");

        builder.Property(jj => jj.JobTypeName);


        builder.HasOne(j => j.Jobs)
            .WithMany(co => co.JobsJobTypes)
            .HasForeignKey(fk => fk.JobId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
