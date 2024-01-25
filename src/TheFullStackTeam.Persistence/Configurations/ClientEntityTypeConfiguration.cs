using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class ClientEntityTypeConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.Property(p => p.Email).HasMaxLength(Client.EmailMaxLength);
        builder.Property(p => p.Name).HasMaxLength(Client.NameMaxLength);
        builder.Property(p => p.Phone).HasMaxLength(Client.PhoneMaxLength);

        builder.Property(p => p.LegalName).HasMaxLength(Client.LegalNameMaxLength);
        builder.Property(p => p.LegalIdentifier);

        builder.HasMany(m => m.Projects).WithOne(o => o.Client).OnDelete(DeleteBehavior.Restrict);
    }
}