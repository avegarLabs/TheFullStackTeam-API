namespace TheFullStackTeam.Domain.Entities;

public class CourseTranslation
{
    public string Occupation { get; set; } = null!;

    public Guid CourseId { get; set; }
    public virtual Course Course { get; set; } = null!;

    public Guid LanguageId { get; set; }
    public virtual Language Language { get; set; } = null!;
}