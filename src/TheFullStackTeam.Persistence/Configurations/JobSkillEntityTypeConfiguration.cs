using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class JobSkillEntityTypeConfiguration : IEntityTypeConfiguration<JobSkill>
{
    public void Configure(EntityTypeBuilder<JobSkill> builder)
    {
        builder.ToTable("JobSkills");
        builder.Property(js => js.SkillName).HasMaxLength(JobSkill.SkillNameMaxLenght);

        builder.HasOne(j => j.Jobs)
            .WithMany(js => js.JobSkills)
            .HasForeignKey(fk => fk.JobId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.Skill)
            .WithMany(js => js.JobSkills)
            .HasForeignKey(fk => fk.SkillId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
