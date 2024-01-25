using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class ProfessionalSkillEntityTypeConfiguration : IEntityTypeConfiguration<ProfessionalSkill>
{
    public void Configure(EntityTypeBuilder<ProfessionalSkill> builder)
    {
        builder.ToTable("ProfessionalSkills");

        builder.Property(p => p.SkillName).HasMaxLength(ProfessionalSkill.SkillNameMaxLenght);


        builder.HasOne(o => o.Professional)
            .WithMany(co => co.ProfessionalSkills)
            .HasForeignKey(fk => fk.ProfessionalId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.Skill)
            .WithMany(co => co.ProfessionalSkills)
            .HasForeignKey(fk => fk.SkillId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
