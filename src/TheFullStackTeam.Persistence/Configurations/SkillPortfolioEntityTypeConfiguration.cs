using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class SkillPortfolioEntityTypeConfiguration : IEntityTypeConfiguration<SkillPortfolio>
{
    public void Configure(EntityTypeBuilder<SkillPortfolio> builder)
    {
        builder.ToTable("SkillPortfolio");

        builder.Property(p => p.SkillVersion);

        builder.HasOne(o => o.Portfolio)
            .WithMany(co => co.SkillPortfolio)
            .HasForeignKey(fk => fk.PortfolioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.Skill)
            .WithMany(co => co.SkillPortsfolio)
            .HasForeignKey(fk => fk.SkillId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
