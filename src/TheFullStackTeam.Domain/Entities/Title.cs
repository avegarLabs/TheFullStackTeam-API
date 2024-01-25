using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

public class Title : BaseEntity
{
    public const int NameMaxLenght = 150;

    public string Name { get; set; } = null!;
    public string OrganizationName { get; set; } = null!;
    public string TitleType { get; set; } = null!;
    public DateTime StartMonthYear { get; set; }
    public DateTime? EndMonthYear { get; set; }

    public Guid? OrganizationId { get; set; }
    public virtual Organization? Organization { get; set; } = null!;

    public Guid? InstitutionId { get; set; }
    public virtual Institution? Institution { get; set; } = null!;

    public Guid ProfessionalId { get; set; }
    public virtual Professional Professional { get; set; } = null!;
}