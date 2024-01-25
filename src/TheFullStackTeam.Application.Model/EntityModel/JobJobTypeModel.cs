using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.EntityModel;

public class JobJobTypeModel
{
    public string JobTypeName { get; set; }

    public static implicit operator JobsJobType(JobJobTypeModel model) => new()
    {
        JobTypeName = model.JobTypeName,
    };
}