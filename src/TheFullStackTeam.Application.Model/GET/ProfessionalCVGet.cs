using System.Linq.Expressions;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.GET;

public class ProfessionalCVGet
{
    public string Moniker { get; set; } = null!;
    public string AboutMe { get; set; } = null!;
    public string Title { get; set; } = null!;
    // public UserProfileGet? UserProfile { get; set; }
    public List<ProfessionalSkillListItem> Skills { get; set; } = new();
    public List<HonorListItem> Honors { get; set; } = new();
    public List<TitleListItem> Titles { get; set; } = new();
    public List<PositionListItem> Positions { get; set; } = new();
    public List<ProfessionalServiceListItem> Services { get; set; } = new();

    public List<ProfessionalContractTypeListItem> Contracts { get; set; } = new();
    public List<ProfessionalJobTypeListItem> Jobs { get; set; } = new();

    public List<ProfessionalSalaryTypeListItem> Salarys { get; set; } = new();


    public static Expression<Func<Professional, ProfessionalCVGet>> Projection =>
        x => new ProfessionalCVGet
        {
            Moniker = x.Moniker,
            AboutMe = x.AboutMe,
            Title = x.Title,
            Skills = x.ProfessionalSkills.AsQueryable().Select(ProfessionalSkillListItem.Projection).ToList(),
            Honors = x.Honors.AsQueryable().Select(HonorListItem.Projection).ToList(),
            Titles = x.Titles.AsQueryable().Select(TitleListItem.Projection).ToList(),
            Positions = x.Experiences.AsQueryable().Select(PositionListItem.Projection).ToList(),
            Services = x.ProfessionalSevices.AsQueryable().Select(ProfessionalServiceListItem.Projection).ToList(),
            Contracts = x.ProfessionalContractTypes.AsQueryable().Select(ProfessionalContractTypeListItem.Projection).ToList(),
            Jobs = x.ProfessionalJobTypes.AsQueryable().Select(ProfessionalJobTypeListItem.Projection).ToList(),
            Salarys = x.ProfessionalSalaryTypes.AsQueryable().Select(ProfessionalSalaryTypeListItem.Projection).ToList(),
            // UserProfile = x.Users
        };
}