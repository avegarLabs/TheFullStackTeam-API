using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

/// <summary>
/// Professional skill entity
/// </summary>
public class ProfessionalSalaryType : BaseEntity
{
    public string PaymentPeriod { get; set; } = null!;
    public double Amount { get; set; }
    public string Currency { get; set; } = null!;

    public Guid ProfessionalId { get; set; }
    public virtual Professional Professional { get; set; } = null!;

}