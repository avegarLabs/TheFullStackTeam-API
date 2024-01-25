namespace TheFullStackTeam.Domain.Entities;

/// <summary>
/// Education entity
/// </summary>
public class Education : Title
{
    public const int DegreeNameMaxLenght = 150;
    public const int NotesMaxLenght = 150;
    public const int ProgramMaxLenght = 150;
    public const int FieldsOfStudyMaxLenght = 250;

    public Education()
    {
        TitleType = nameof(Education);
    }

    public string DegreeName { get; set; } = null!;
    public string? Notes { get; set; }
    public string? Program { get; set; }
    public string? FieldsOfStudy { get; set; }
}