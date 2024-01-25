using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

public class ProfessionalServiceCategory : BaseEntity
{
    public Guid ProfessionalServiceId { get; set; }

    public virtual ProfessionalSevices ProfessionalSevice { get; set; }

    public Guid CategoryId { get; set; }

    public virtual Category Category { get; set; }

}
