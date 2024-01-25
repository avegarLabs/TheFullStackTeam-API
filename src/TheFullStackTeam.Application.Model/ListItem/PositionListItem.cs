using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem
{
    public class PositionListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartMonthYear { get; set; }
        public DateTime EndMonthYear { get; set; }
        public string? OrganizationName { get; set; }
        public Guid? OrganizationId { get; set; }
        public string? Type { get; set; }
        public Guid ProfessionalId { get; set; }

        public List<SkillListItem> SkillPositionLists { get; set; } = new();

        public static implicit operator PositionListItem(Position domainEntity) => new()
        {
            Id = domainEntity.Id,
            Name = domainEntity.Name,
            Description = domainEntity.Description,
            StartMonthYear = DateTime.Parse(domainEntity.StartMonthYear.ToString()),
            EndMonthYear = DateTime.Parse(domainEntity.EndMonthYear.ToString()),
            OrganizationName = domainEntity.OrganizationId != null ? domainEntity.Organization.Name : domainEntity.Institution.Name,
            OrganizationId = domainEntity.OrganizationId != null ? domainEntity.OrganizationId : domainEntity.InstitutionId,
            Type = domainEntity.OrganizationId != null ? "org" : "inst",
            ProfessionalId = domainEntity.ProfessionalId
        };

        public static Expression<Func<Position, PositionListItem>> Projection =>
            x => new PositionListItem
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                StartMonthYear = DateTime.Parse(x.StartMonthYear.ToString()),
                EndMonthYear = DateTime.Parse(x.EndMonthYear.ToString()),
                OrganizationName = x.OrganizationId != null ? x.Organization.Name : x.Institution.Name,
                OrganizationId = x.OrganizationId != null ? x.OrganizationId : x.InstitutionId,
                Type = x.OrganizationId != null ? "org" : "inst",
                ProfessionalId = x.ProfessionalId,
                SkillPositionLists = x.SkillPositions.AsQueryable().Select(SkillListItem.Projection).ToList()
            };
    }
}
