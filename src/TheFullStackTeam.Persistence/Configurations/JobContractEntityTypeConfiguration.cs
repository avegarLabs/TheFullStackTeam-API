using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class JobContractEntityTypeConfiguration : IEntityTypeConfiguration<JobContractType>
{
    public void Configure(EntityTypeBuilder<JobContractType> builder)
    {
        builder.ToTable("JobContratType");

        builder.Property(jc => jc.ContractTypeName).HasMaxLength(JobContractType.ContractTypeNameMaxLenght);


        builder.HasOne(o => o.Jobs)
            .WithMany(co => co.JobContractTypes)
            .HasForeignKey(fk => fk.JobId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
