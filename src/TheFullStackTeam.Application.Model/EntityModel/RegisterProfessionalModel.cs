namespace TheFullStackTeam.Application.Model.EntityModel;

public class RegisterProfessionalModel
{
    public string Name { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string AboutMe { get; set; } = null!;
    public string Industry { get; set; } = null!;
    public string AccountId { get; set; } = null!;

    public IEnumerable<ProfessionalSkillModel> Skills { get; set; }
}