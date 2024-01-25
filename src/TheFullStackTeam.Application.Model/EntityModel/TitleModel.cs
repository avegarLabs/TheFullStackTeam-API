using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.EntityModel
{
    public class TitleModel
    {
        public string Name { get; set; } = null!;
        public string TitleType { get; set; } = null!;
        public string OrganizationName { get; set; } = null!;
        public string OrganizationMoniker { get; set; } = null!;
        public DateTime StartMonthYear { get; set; }
        public DateTime? EndMonthYear { get; set; }
        public string Type { get; set; } = null!;

        public static implicit operator Title(TitleModel model) => new()
        {
            Name = model.Name,
            TitleType = model.TitleType,
            StartMonthYear = model.StartMonthYear,
            EndMonthYear = model.EndMonthYear,
            OrganizationName = model.OrganizationName,
        };
    }
}
