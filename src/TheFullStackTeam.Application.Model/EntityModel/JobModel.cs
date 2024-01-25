using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Model.EntityModel;

public class JobModel
{
    public string JobTitle { get; set; } = null!;
    public string JobDescription { get; set; } = null!;

    public Guid OwnerId { get; set; }

    public int Type { get; set; }

    public List<JobSkillModel> JobSkills { get; set; }

    public  List<JobContractTypeModel> JobContractTypes { get; set; }

    public  List<JobJobTypeModel> JobsJobTypes { get; set; }

    public  JobSalaryTypeModel JobsSalaryTypes { get; set; }

    public  List<string> LanguagesRequired { get; set; }

    public  List<JobResposabilitiesModel> JobResponsabilities { get; set; }

    public Guid CountryId { get; set; }
}
