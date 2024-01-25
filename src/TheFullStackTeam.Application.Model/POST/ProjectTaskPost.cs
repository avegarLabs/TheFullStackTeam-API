using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.POST;

public class ProjectTaskPost
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public static implicit operator ProjectTask(ProjectTaskPost model)
        => new()
        {
            Name = model.Name,
            Description = model.Description
        };
}