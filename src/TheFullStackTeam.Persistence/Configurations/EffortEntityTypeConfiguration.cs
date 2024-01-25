using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class EffortEntityTypeConfiguration : IEntityTypeConfiguration<Effort>
{
    public void Configure(EntityTypeBuilder<Effort> builder)
    {
        builder.Property(p => p.Notes).HasMaxLength(Effort.NotesMaxLength);
        builder.Property(p => p.Value).HasPrecision(18, 1);
    }
}