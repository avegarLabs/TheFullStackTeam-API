using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

public class Position : BaseEntity
{
    public const int NameMaxLenght = 150;
    public const int DescriptionMaxLenght = 1024;

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public DateTime? StartMonthYear { get; set; }
    public DateTime? EndMonthYear { get; set; }

    public Guid ProfessionalId { get; set; }
    public virtual Professional Professional { get; set; } = null!;

    public Guid? OrganizationId { get; set; }
    public virtual Organization? Organization { get; set; } = null!;

    public Guid? InstitutionId { get; set; }
    public virtual Institution? Institution { get; set; } = null!;

    public virtual ICollection<Skill> SkillPositions { get; set; } = new List<Skill>();
}