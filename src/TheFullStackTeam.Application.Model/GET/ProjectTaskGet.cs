using System.Linq.Expressions;
using TheFullStackTeam.Application.Model.Base;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.GET;

public class ProjectTaskGet : NickNamedModel
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string ProjectName { get; set; } = null!;
    public string ProjectMoniker { get; set; } = null!;

    public static Expression<Func<ProjectTask, ProjectTaskGet>> Projection =>
        x => new ProjectTaskGet
        {
            Moniker = x.Moniker,
            Name = x.Name,
            Description = x.Description,
            ProjectName = x.Project.Name,
            ProjectMoniker = x.Project.Moniker
        };

    public static implicit operator ProjectTaskGet(ProjectTask model) => new()
    {
        Name = model.Name,
        Description = model.Description,
    };
}