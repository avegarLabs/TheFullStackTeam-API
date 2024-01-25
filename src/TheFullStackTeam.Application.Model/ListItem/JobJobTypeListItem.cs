using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem;

public class JobJobTypeListItem
{
    public Guid Id { get; set; }
    public string JobTypeName { get; set; } = null!;

    public static implicit operator JobJobTypeListItem(JobsJobType domainEntity) => new()
    {
        Id = domainEntity.Id,
        JobTypeName = domainEntity.JobTypeName,
    };

    public static Expression<Func<JobsJobType, JobJobTypeListItem>> Projection =>
        x => new JobJobTypeListItem
        {
            Id = x.Id,
            JobTypeName = x.JobTypeName,
          
        };
}