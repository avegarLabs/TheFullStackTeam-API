namespace TheFullStackTeam.Domain.Entities;

public class ExperienceTranslation
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public Guid ExperienceId { get; set; }
    public virtual Position Experience { get; set; } = null!;

    public Guid LanguageId { get; set; }
    public virtual Language Language { get; set; } = null!;
}
