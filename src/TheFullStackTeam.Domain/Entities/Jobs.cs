using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

/// <summary>
/// Job entity
/// </summary>
public class Job : NicknamedEntity
{
    public const int JobTitleLenght = 1024;
    public string JobTitle { get; set; } = null!;
    public string JobDescription { get; set; } = null!;

    public virtual ICollection<JobSkill> JobSkills { get; set; } = null!;

    public virtual ICollection<JobContractType> JobContractTypes { get; set; } = null!;

    public virtual ICollection<JobsJobType> JobsJobTypes { get; set; } = null!;

    public virtual ICollection<JobsSalaryType> JobsSalaryTypes { get; set; } = null!;

   public virtual ICollection<JobLanguage> RequiredLanguages { get; set; } = new List<JobLanguage>();

    public virtual ICollection<JobResponsabilities> JobResponsabilities { get; set; } = null!;

    public Guid? OrganizationId { get; set; }
    public virtual Organization? Organization { get; set; } = null!;

    public Guid? ProfessionalId { get; set; }
    public virtual Professional? Professional { get; set; } = null!;

    [DefaultValue(false)]
    public bool Active { get; set; }

    public Guid? CountryId { get; set; }

    public virtual Country? Country { get; set; }

}