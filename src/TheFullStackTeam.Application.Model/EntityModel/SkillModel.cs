using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.EntityModel;

/// <summary>
/// Skill post model
/// </summary>
public class SkillModel
{
    /// <summary>
    /// Skill name
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Skill categories
    /// </summary>
    public List<Guid> Categories { get; set; } = new();

    /// <summary>
    /// Cast to skill entity
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public static implicit operator Skill(SkillModel model) => new()
    {
        Name = model.Name,
        SkillCategories = model.Categories.Select(s => new SkillCategory { CategoryId = s }).ToList()
    };
}