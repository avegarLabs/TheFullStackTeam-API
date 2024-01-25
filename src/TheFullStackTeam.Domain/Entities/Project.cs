using System.ComponentModel.DataAnnotations;
using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

public class Project : NicknamedEntity
{
    public const int NameMaxLength = 100;
    public const int DescriptionMaxLength = 255;

    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public Guid? ClientId { get; set; }
    public virtual Client? Client { get; set; }

    public Guid? ProfessionalId { get; set; }
    public virtual Professional? Professional { get; set; } = null!;

    public Guid? OrganizationId { get; set; }
    public virtual Organization? Organization { get; set; } = null!;

    public virtual ICollection<ProjectTask> ProjectTasks { get; set; } = null!;
    public virtual ICollection<Contracts> Contracts { get; set; } = null!;

    public DateTime? DueDate { get; set; }


    [MaxLength(1)]
    public string? Active { get; set; }

}