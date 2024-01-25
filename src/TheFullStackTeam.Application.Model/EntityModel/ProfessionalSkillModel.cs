using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.EntityModel
{
    public class ProfessionalSkillModel
    {
        public string Name { get; set; } = null!;
        public string? SkillMoniker { get; set; }
        public int Level { get; set; }

        public static implicit operator ProfessionalSkill(ProfessionalSkillModel model) => new()
        {
            SkillName = model.Name,
            SkillLevel = model.Level,
        };
    }
}
