using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

/// <summary>
/// Honor entity
/// </summary>
public class Honor : BaseEntity
{
    public const int TitleMaxLenght = 150;
    public const int DescriptionMaxLenght = 1024;

    public string Title { get; set; } = null!;
    public string OrganizationName { get; set; } = null!;

    public string Description { get; set; } = null!;
    public DateTime IssueDate { get; set; }

    public Guid? OrganizationId { get; set; }
    public virtual Organization? Organization { get; set; } = null!;

    public Guid ProfessionalId { get; set; }
    public virtual Professional Professional { get; set; } = null!;
}