using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class ProfessionalServiceCategoryEntityTypeConfiguration : IEntityTypeConfiguration<ProfessionalServiceCategory>
{
    public void Configure(EntityTypeBuilder<ProfessionalServiceCategory> builder)
    {
        builder.ToTable("ProfessionalServiceCategories");


        builder.HasOne(o => o.ProfessionalSevice)
            .WithMany(co => co.ProfessionalServiceCategories)
            .HasForeignKey(fk => fk.ProfessionalServiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.Category)
            .WithMany(co => co.ProfessionalServiceCategories)
            .HasForeignKey(fk => fk.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
