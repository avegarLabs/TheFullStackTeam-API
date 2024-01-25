using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class ProfessionalServicesEntityTypeConfiguration : IEntityTypeConfiguration<ProfessionalSevices>
{
    public void Configure(EntityTypeBuilder<ProfessionalSevices> builder)
    {
        builder.ToTable("ProfessionalServices");

        builder.Property(p => p.ServiceName).HasMaxLength(ProfessionalSevices.ServiceNameMaxLenght);
        builder.Property(p => p.ServiceDescription);
        builder.Property(p => p.SevicePrice);
        builder.Property(p => p.Currency);
       

        builder.HasOne(o => o.Professional)
            .WithMany(co => co.ProfessionalSevices)
            .HasForeignKey(fk => fk.ProfessionalId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
