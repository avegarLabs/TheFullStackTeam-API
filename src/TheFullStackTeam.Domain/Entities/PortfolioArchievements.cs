using TheFullStackTeam.Domain.Entities.Base;
using TheFullStackTeam.Domain.ValueObjects;

namespace TheFullStackTeam.Domain.Entities
{
    public class PortfolioArchievements : BaseEntity
    {
        public virtual Portfolio Portfolio { get; set; }
        public Guid PortfolioId { get; set; }

        public ImageUrl? Archive { get; set; } = null!;
    }
}
