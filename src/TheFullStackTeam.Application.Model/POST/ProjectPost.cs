using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.POST;

public class ProjectPost
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string ClientMoniker { get; set; } = null!;

    public static implicit operator Project(ProjectPost model)
        => new()
        {
            Name = model.Name,
            Description = model.Description,
        };
}