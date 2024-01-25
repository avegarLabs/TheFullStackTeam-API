using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class JobSalaryEntityTypeConfiguration : IEntityTypeConfiguration<JobsSalaryType>
{
    public void Configure(EntityTypeBuilder<JobsSalaryType> builder)
    {
        builder.ToTable("JobsSalaryType");
        builder.Property(ps => ps.SalaryTypeName);
        builder.Property(ps => ps.MinAmount);
        builder.Property(ps => ps.MaxAmount);
        builder.Property(ps => ps.Currency);

        builder.HasOne(o => o.Jobs)
            .WithMany(co => co.JobsSalaryTypes)
            .HasForeignKey(fk => fk.JobId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
