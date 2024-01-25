using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

public class OrganizationServiceCategory : BaseEntity
{
    public Guid OrganizationSevicesId { get; set; }

    public virtual OrganizationSevices OrganizationSevice { get; set; }

    public Guid CategoryId { get; set; }

    public virtual Category Category { get; set; }

}
