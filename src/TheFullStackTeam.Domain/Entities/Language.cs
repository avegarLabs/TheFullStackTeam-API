using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

/// <summary>
/// Language entity
/// </summary>
public class Language : BaseEntity
{
    public const int NameMaxLenght = 100;
    public const int LocalNameMaxLenght = 50;
    public const int IsoCodeMaxLenght = 10;
    public const int ThreeLetterIsoCodeMaxLenght = 3;

    public string Name { get; set; } = null!;
    public string LocalName { get; set; } = null!;
    public string IsoCode { get; set; } = null!;
    public string ThreeLetterIsoCode { get; set; } = null!;
    public int LCID { get; set; }
    
    public virtual ICollection<ProfessionalLanguage> ProfessionalLanguages { get; set; } = null!;
}