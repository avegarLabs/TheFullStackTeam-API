namespace TheFullStackTeam.Domain.Entities;

/// <summary>
/// Translations of honor entity
/// </summary>
public class HonorTranslation
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;

    public Guid HonorId { get; set; }
    public virtual Honor Honor { get; set; } = null!;

    public Guid LanguageId { get; set; }
    public virtual Language Language { get; set; } = null!;
}