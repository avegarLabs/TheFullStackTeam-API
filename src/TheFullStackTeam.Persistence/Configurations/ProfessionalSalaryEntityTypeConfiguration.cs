using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class ProfessionalSalaryEntityTypeConfiguration : IEntityTypeConfiguration<ProfessionalSalaryType>
{
    public void Configure(EntityTypeBuilder<ProfessionalSalaryType> builder)
    {
        builder.ToTable("ProfessionalSalaryType");
        builder.Property(p => p.PaymentPeriod);
        builder.Property(p => p.Amount);
        builder.Property(p => p.Currency);

        builder.HasOne(o => o.Professional)
            .WithMany(co => co.ProfessionalSalaryTypes)
            .HasForeignKey(fk => fk.ProfessionalId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
