using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class OrganizationServiceCategoryEntityTypeConfiguration : IEntityTypeConfiguration<OrganizationServiceCategory>
{
    public void Configure(EntityTypeBuilder<OrganizationServiceCategory> builder)
    {
        builder.ToTable("OrganizationServiceCategories");


        builder.HasOne(o => o.OrganizationSevice)
            .WithMany(co => co.OrganizationServiceCategories)
            .HasForeignKey(fk => fk.OrganizationSevicesId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.Category)
            .WithMany(co => co.OrganizationServiceCategories)
            .HasForeignKey(fk => fk.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}


