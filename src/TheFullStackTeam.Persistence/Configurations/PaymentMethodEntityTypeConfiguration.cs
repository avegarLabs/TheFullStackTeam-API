using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class PaymentMethodEntityTypeConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.ToTable("PaymentMethods");

        builder.HasIndex(p => p.BankAccount).IsUnique();

        builder.Property(p => p.BankAccount).HasMaxLength(PaymentMethod.BankAccountMaxLenght);
        builder.Property(p => p.Description).HasMaxLength(PaymentMethod.DescriptionMaxLenght);

        builder.HasOne(o => o.Professional)
           .WithMany(co => co.PaymentMethods)
           .HasForeignKey(fk => fk.ProfessionalId)
           .OnDelete(DeleteBehavior.Restrict);
    }
}
