using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

/// <summary>
/// UserProfile entity type configuration
/// </summary>
public class ContractsEntityTypeConfiguration : IEntityTypeConfiguration<Contracts>
{
    public void Configure(EntityTypeBuilder<Contracts> builder)
    {
        builder.ToTable("Contrats");


        builder.Property(p => p.StartDate);
        builder.Property(p => p.EndDate);
        builder.Property(p => p.SalaryRate);
        builder.Property(p => p.FreeDays);
        builder.Property(p => p.ContractsDetails);

        builder.HasOne(o => o.Project)
          .WithMany(co => co.Contracts)
          .HasForeignKey(fk => fk.ProjectId)
          .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.Professional)
         .WithMany(co => co.Contracts)
         .HasForeignKey(fk => fk.ProfessionalId)
         .OnDelete(DeleteBehavior.Restrict);
    }
}