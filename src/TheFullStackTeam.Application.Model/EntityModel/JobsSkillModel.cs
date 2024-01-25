using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.EntityModel;

public class JobSkillModel
{
    public string SkillName { get; set; } = null!;
    public string? SkillMoniker { get; set; }
    
    public static implicit operator JobSkill(JobSkillModel model) => new()
    {
        SkillName = model.SkillName,
    };
}