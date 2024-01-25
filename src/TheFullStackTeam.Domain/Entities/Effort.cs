using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

public class Effort : BaseEntity
{
    public const int NotesMaxLength = 255;

    public DateTime EffortDate { get; set; }
    public decimal Value { get; set; }
    public string? Notes { get; set; }
    public bool Billable { get; set; }

    public Guid ProjectTaskId { get; set; }
    public virtual ProjectTask ProjectTask { get; set; } = null!;
}