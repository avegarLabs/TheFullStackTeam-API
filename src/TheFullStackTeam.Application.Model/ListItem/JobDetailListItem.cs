using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem;

public class JobDetailListItem
{
    public Guid Id { get; set; }
    public string JobTitle { get; set; } = null!;
    public string JobDescription { get; set; } = null!;
   
    public string Moniker { get; set; } = null!;
    public virtual List<JobSkillListItem> JobSkills { get; set; } = new List<JobSkillListItem>();

    public virtual List<JobContractTypeListItem> JobContractTypes { get; set; } = new List<JobContractTypeListItem>();

    public virtual List<JobJobTypeListItem> JobsJobTypes { get; set; } = new List<JobJobTypeListItem>();

    public virtual List<JobSalaryTypeListItem> JobsSalaryTypes { get; set; } = new List<JobSalaryTypeListItem>();

    public virtual List<JobLanguagueListItem> JobLanguagues { get; set; } = new List<JobLanguagueListItem>();

    public virtual List<JobResponsabilitiesListItem> JobResponsabilities { get; set; } = new List<JobResponsabilitiesListItem>();
    public bool State { get; set; }
    public string OwnerName { get; set; } = null!;
    public Guid OwnerId { get; set; }

    public CountryListItem Country { get; set; }


    public static Expression<Func<Job, JobDetailListItem>> Projection =>
        x => new JobDetailListItem
        {
            Id = x.Id,
            JobTitle = x.JobTitle,
            JobDescription = x.JobDescription,
            Moniker = x.Moniker,
            JobSkills = x.JobSkills.AsQueryable().Select(JobSkillListItem.Projection).ToList(),
           JobContractTypes = x.JobContractTypes.AsQueryable().Select(JobContractTypeListItem.Projection).ToList(),
           JobsJobTypes = x.JobsJobTypes.AsQueryable().Select(JobJobTypeListItem.Projection).ToList(),
           JobLanguagues = x.RequiredLanguages.AsQueryable().Select(JobLanguagueListItem.Projection).ToList(),
           JobsSalaryTypes = x.JobsSalaryTypes.AsQueryable().Select(JobSalaryTypeListItem.Projection).ToList(),
           JobResponsabilities = x.JobResponsabilities.AsQueryable().Select(JobResponsabilitiesListItem.Projection).ToList(),
            State = x.Active,
            OwnerName = x.ProfessionalId != null? x.Professional.Name : x.Organization.Name,
            OwnerId = x.ProfessionalId != null ? x.Professional.Id : x.Organization.Id,
            Country = x.Country
        };
}