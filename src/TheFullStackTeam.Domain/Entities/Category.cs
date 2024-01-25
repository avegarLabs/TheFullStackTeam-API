using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

public class Category : BaseEntity
{
    public const int NameMaxLenght = 50;

    public string Name { get; set; } = null!;

    public virtual ICollection<SkillCategory> SkillCategories { get; set; } = null!;
    public virtual ICollection<ProfessionalServiceCategory> ProfessionalServiceCategories { get; set; } = null!;
    public virtual ICollection<OrganizationServiceCategory> OrganizationServiceCategories { get; set; } = null!;
}
