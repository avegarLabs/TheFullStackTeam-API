using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities
{
    public class Portfolio : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public Guid ProfessionalId;
        public virtual Professional Professional { get; set; }

        public virtual ICollection<SkillPortfolio> SkillPortfolio { get; set; } = null!;
        public virtual ICollection<PortfolioArchievements> PortfolioAchievements { get; set; } = null!;

    }
}
