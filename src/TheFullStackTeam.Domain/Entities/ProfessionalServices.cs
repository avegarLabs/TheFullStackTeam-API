using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

/// <summary>
/// Professional service entity
/// </summary>
public class ProfessionalSevices : NicknamedEntity
{
    public const int ServiceNameMaxLenght = 250;

    public string ServiceName { get; set; } = null!;
    public double SevicePrice { get; set; }
    public string Currency { get; set; } = null!;

    public string ServiceDescription { get; set; } = null!;

    public Guid ProfessionalId { get; set; }
    public virtual Professional Professional { get; set; } = null!;

    public virtual ICollection<ProfessionalServiceCategory> ProfessionalServiceCategories { get; set; } = null!;

    public virtual ICollection<Skill> ServiceSkills { get; set; } = new List<Skill>();

}