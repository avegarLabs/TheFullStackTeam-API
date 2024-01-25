using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class OrganizationServicesEntityTypeConfiguration : IEntityTypeConfiguration<OrganizationSevices>
{
    public void Configure(EntityTypeBuilder<OrganizationSevices> builder)
    {
        builder.ToTable("OrganizationSevices");

        builder.Property(p => p.ServiceName).HasMaxLength(ProfessionalSevices.ServiceNameMaxLenght);
        builder.Property(p => p.ServiceDescription);
        builder.Property(p => p.SevicePrice);
        builder.Property(p => p.Currency);
       
        builder.HasOne(o => o.Organization)
            .WithMany(co => co.OrganizationSevices)
            .HasForeignKey(fk => fk.OrganizationId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
