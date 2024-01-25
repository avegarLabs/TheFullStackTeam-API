using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem;

public class JobResponsabilitiesListItem
{
    public Guid Id { get; set; }
    public string ResponsibilityDescription { get; set; }

    public static implicit operator JobResponsabilitiesListItem(JobResponsabilities domainEntity) => new()
    {
        Id = domainEntity.Id,
        ResponsibilityDescription = domainEntity.ResposabilityDescription
    };

    public static Expression<Func<JobResponsabilities, JobResponsabilitiesListItem>> Projection =>
        ps => new JobResponsabilitiesListItem
        {
            Id = ps.Id,
            ResponsibilityDescription = ps.ResposabilityDescription
        };
}