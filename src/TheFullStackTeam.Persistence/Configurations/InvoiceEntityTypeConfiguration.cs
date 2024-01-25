using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class InvoiceEntityTypeConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Invoice");

        builder.HasIndex(p => p.Number).IsUnique();
        builder.HasIndex(p => p.Moniker).IsUnique();

        builder.Property(p => p.Number);
        builder.Property(p => p.Emitter);
        builder.Property(p => p.Reciever);
        builder.Property(p => p.Amount);
        builder.Property(p => p.IssueDates);
        builder.Property(p => p.ExpirationDate);
        builder.Property(p => p.PayDetail);
        builder.Property(p => p.State);

        builder.HasOne(o => o.Professional)
            .WithMany(co => co.Invoices)
            .HasForeignKey(fk => fk.ProfessionalId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.Timesheet)
           .WithOne(p => p.Invoice)
           .HasForeignKey<Timesheet>(or => or.InvoiceId);
    }
}
