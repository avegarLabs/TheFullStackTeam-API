using System.Linq.Expressions;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem;

public class JobListItem
{
    public Guid Id { get; set; }
    public string JobTitle { get; set; } = null!;
    public string JobDescription { get; set; } = null!;

    public string Moniker { get; set; } = null!;

    public List<JobContractTypeModel> JobContractTypes { get; set; }

    public List<JobJobTypeModel> JobsJobTypes { get; set; }

     public JobSalaryTypeListItem JobsSalaryTypes { get; set; } = null!;

    public List<JobLanguagueListItem> JobLanguagues { get; set; } = null!;

    public List<JobSkillModel> JobSkills { get; set; } = null!;
    public List<JobResposabilitiesModel> JobResponsabilities { get; set; } = null!;


    public static implicit operator JobListItem(Job domainEntity) => new()
    {
        Id = domainEntity.Id,
        JobTitle = domainEntity.JobTitle,
        JobDescription = domainEntity.JobDescription,
        Moniker = domainEntity.Moniker
       
    };

    public static Expression<Func<Job, JobListItem>> Projection =>
        j => new JobListItem
        {
            Id = j.Id,
            JobTitle = j.JobTitle,
            JobDescription = j.JobDescription,
            Moniker = j.Moniker
        };

}
