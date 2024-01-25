namespace TheFullStackTeam.Domain.Entities;

public class ProfessionalTranslation
{
    public string AboutMe { get; set; } = null!;
    public string HeadLine { get; set; } = null!;

    public Guid ProfessionalId { get; set; }
    public virtual Professional Professional { get; set; } = null!;

    public Guid LanguageId { get; set; }
    public virtual Language Language { get; set; } = null!;
}