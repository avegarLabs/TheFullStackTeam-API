namespace TheFullStackTeam.Domain.Entities;

/// <summary>
/// Translations of education
/// </summary>
public class EducationTranslation
{
    public string DegreeName { get; set; } = null!;
    public string? Program { get; set; }
    public string? FieldsOfStudy { get; set; }

    public Guid EducationId { get; set; }
    public virtual Education Education { get; set; } = null!;

    public Guid LanguageId { get; set; }
    public virtual Language Language { get; set; } = null!;
}