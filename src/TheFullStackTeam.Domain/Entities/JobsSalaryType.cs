using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

/// <summary>
/// Job salay entity
/// </summary>
public class JobsSalaryType : BaseEntity
{
    public string SalaryTypeName { get; set; } = null!;
    public string Currency { get; set; } = null!;
    public double MinAmount { get; set; }
    public double MaxAmount { get; set; }

    public Guid JobId { get; set; }
    public virtual Job Jobs { get; set; } = null!;
}