using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class PositionEntityTypeConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("Position");

        builder.Property(p => p.Name).HasMaxLength(Position.NameMaxLenght);
        builder.Property(p => p.Description).HasMaxLength(Position.DescriptionMaxLenght);

        builder.HasOne(o => o.Professional)
       .WithMany(co => co.Experiences)
       .HasForeignKey(fk => fk.ProfessionalId)
       .OnDelete(DeleteBehavior.Cascade);

     builder.HasOne(o => o.Organization)
    .WithMany(co => co.Positions)
    .HasForeignKey(fk => fk.OrganizationId)
    .OnDelete(DeleteBehavior.Cascade);
    }
}
