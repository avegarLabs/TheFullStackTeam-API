using TheFullStackTeam.Domain.Entities.Base;
using TheFullStackTeam.Domain.ValueObjects;

namespace TheFullStackTeam.Domain.Entities;

/// <summary>
/// Professional entity
/// </summary>
public class Professional : NicknamedEntity
{
    public const int NameMaxLenght = 150;
    public const int AboutMeMaxLenght = 1024;
    public const int HeadLineMaxLenght = 500;
    public const int PhoneMaxLenght = 20;
    public const int ContactEmailMaxLenght = 150;

    public string Name { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string AboutMe { get; set; } = null!;

    public string Industry { get; set; } = null!;

    public string Phone { get; set; } = null!;
    public string ContactEmail { get; set; } = null!;

    public ImageUrl? Picture { get; set; } = null!;

    public string PersonalWeb { get; set; } = null!;

    public string LinkedInProfile { get; set; } = null!;

    public string YoutubeProfile { get; set; } = null!;

    public Guid? CountryId { get; set; }

    public string? VitaePath { get; set; }
    public string? VitaeId { get; set; }

    public virtual Country? Country { get; set; }

    public Guid UserId { get; set; }
    public virtual User User { get; set; } = null!;

    public virtual ICollection<ProfessionalSkill> ProfessionalSkills { get; set; } = null!;
    public virtual ICollection<Title> Titles { get; set; } = null!;
    public virtual ICollection<Position> Experiences { get; set; } = null!;
    public virtual ICollection<Honor> Honors { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; set; } = null!;

    public virtual ICollection<ProfessionalSevices> ProfessionalSevices { get; set; } = null!;

    public virtual ICollection<Portfolio> Portfolios { get; set; } = null!;

    public virtual ICollection<ProfessionalContractType> ProfessionalContractTypes { get; set; } = null!;

    public virtual ICollection<ProfessionalJobType> ProfessionalJobTypes { get; set; } = null!;

    public virtual ICollection<ProfessionalSalaryType> ProfessionalSalaryTypes { get; set; } = null!;

    public virtual ICollection<Job> Jobs { get; set; } = null!;

    public Availability? Availability { get; set; } = null!;

    public virtual ICollection<Contracts> Contracts { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; }

    public virtual ICollection<PaymentMethod> PaymentMethods { get; set; }

    public virtual ICollection<ProfessionalLanguage> ProfessionalLanguages { get; set; } = null!;

}