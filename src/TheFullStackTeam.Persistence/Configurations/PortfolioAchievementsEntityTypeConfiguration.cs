using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class PortfolioAchievementsEntityTypeConfiguration : IEntityTypeConfiguration<PortfolioArchievements>
{
    public void Configure(EntityTypeBuilder<PortfolioArchievements> builder)
    {
        builder.ToTable("PortfolioArchievements");

        builder.HasOne(o => o.Portfolio)
            .WithMany(co => co.PortfolioAchievements)
            .HasForeignKey(fk => fk.PortfolioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.OwnsOne(a => a.Archive).WithOwner();

    }
}
