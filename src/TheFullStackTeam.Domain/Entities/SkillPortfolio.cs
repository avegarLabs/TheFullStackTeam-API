using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities
{
    public class SkillPortfolio : BaseEntity
    {
        public virtual Skill Skill { get; set; }
        public Guid SkillId { get; set; }

        public string SkillVersion { get; set; } = null!;

        public virtual Portfolio Portfolio { get; set; }
        public Guid PortfolioId { get; set; }
    }
}
