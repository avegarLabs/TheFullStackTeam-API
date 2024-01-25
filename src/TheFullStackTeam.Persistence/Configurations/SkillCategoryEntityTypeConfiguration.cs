using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations
{
    public class SkillCategoryEntityTypeConfiguration : IEntityTypeConfiguration<SkillCategory>
    {
        public void Configure(EntityTypeBuilder<SkillCategory> builder)
        {
            builder.ToTable("SkillCategories");


            builder.HasOne(o => o.Skill)
                .WithMany(co => co.SkillCategories)
                .HasForeignKey(fk => fk.SkillId);

            builder.HasOne(o => o.Category)
                .WithMany(co => co.SkillCategories)
                .HasForeignKey(fk => fk.CategoryId);
        }
    }
}
