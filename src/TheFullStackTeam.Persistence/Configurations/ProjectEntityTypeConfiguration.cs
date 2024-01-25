using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.Property(p => p.Name).HasMaxLength(Project.NameMaxLength);
        builder.Property(p => p.Description).HasMaxLength(Project.DescriptionMaxLength);
        builder.Property(p => p.Active).HasDefaultValueSql("1");

        builder.HasOne(o => o.Professional)
           .WithMany(co => co.Projects)
           .HasForeignKey(fk => fk.ProfessionalId)
           .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.Organization)
          .WithMany(co => co.Projects)
          .HasForeignKey(fk => fk.OrganizationId)
          .OnDelete(DeleteBehavior.Cascade);
    }
}