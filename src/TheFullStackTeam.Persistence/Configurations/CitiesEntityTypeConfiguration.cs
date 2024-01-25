using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class CitiesEntityTypeConfiguration : IEntityTypeConfiguration<Cities>
{
    public void Configure(EntityTypeBuilder<Cities> builder)
    {
        builder.ToTable("Cities");

        builder.Property(p => p.Name).HasMaxLength(Skill.NameMaxLenght);

        builder.HasOne(o => o.Country)
          .WithMany(co => co.Cities)
          .HasForeignKey(fk => fk.CountryId)
          .OnDelete(DeleteBehavior.Cascade);

    }
}
