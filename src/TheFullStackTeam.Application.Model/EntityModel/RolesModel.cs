using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.EntityModel;

/// <summary>
/// Skill post model
/// </summary>
public class RolesModel
{
    /// <summary>
    /// Role name
    /// </summary>
    public string Name { get; set; } = null!;


    /// <summary>
    /// Cast to Role entity
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public static implicit operator Roles(RolesModel model) => new()
    {
        Name = model.Name,
       
    };
}