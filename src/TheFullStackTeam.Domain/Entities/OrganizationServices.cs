using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

public class OrganizationSevices: NicknamedEntity
{
    public const int ServiceNameMaxLenght = 250;

    public string ServiceName { get; set; } = null!;
    public double SevicePrice { get; set; }
    public string Currency { get; set; } = null!;

    public string ServiceDescription { get; set; } = null!;

    public Guid OrganizationId { get; set; }
    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<Skill> ServiceSkills { get; set; } = new List<Skill>();
    public virtual ICollection<OrganizationServiceCategory> OrganizationServiceCategories { get; set; } = null!;
}