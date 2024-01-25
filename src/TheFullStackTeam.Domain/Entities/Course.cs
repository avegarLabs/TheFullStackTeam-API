namespace TheFullStackTeam.Domain.Entities;

/// <summary>
/// Course entity
/// </summary>
public class Course : Title
{
    public const int CourseNumberMaxLenght = 150;
    public const int OccupationMaxLenght = 150;

    public Course()
    {
        TitleType = nameof(Course);
    }
    public string CourseNumber { get; set; } = null!;
    public string Occupation { get; set; } = null!;
}