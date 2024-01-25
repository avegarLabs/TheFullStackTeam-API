using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class JobEntityTypeConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.ToTable("Jobs");

        builder.Property(p => p.JobTitle).HasMaxLength(Job.JobTitleLenght);
        builder.Property(p => p.JobDescription);
        builder.HasIndex(p => p.Moniker).IsUnique();
        builder.Property(p => p.Active);
      
        builder.HasOne(o => o.Professional)
           .WithMany(co => co.Jobs)
           .HasForeignKey(fk => fk.ProfessionalId)
           .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.Organization)
          .WithMany(co => co.Jobs)
          .HasForeignKey(fk => fk.OrganizationId)
          .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.Country)
         .WithMany(co => co.Jobs)
         .HasForeignKey(fk => fk.CountryId)
         .OnDelete(DeleteBehavior.Restrict);
    }
}
