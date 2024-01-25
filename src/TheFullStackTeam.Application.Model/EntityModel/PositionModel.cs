using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.EntityModel
{
    public class PositionModel
    {
        public string Description { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime StartMonthYear { get; set; }
        public DateTime? EndMonthYear { get; set; }
        public string OrganizationName { get; set; } = null!;
        public string OrganizationMoniker { get; set; } = null!;
        public string Type { get; set; } = null!;

        public IEnumerable<string> SkillList { get; set; }


        public static implicit operator Position(PositionModel model) => new()
        {
            Description = model.Description,
            Name = model.Name,
            StartMonthYear = model.StartMonthYear,
            EndMonthYear = model.EndMonthYear,
        };
    }
}
