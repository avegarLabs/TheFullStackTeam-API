using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.EntityModel;

public class JobResposabilitiesModel
{
    public string ResponsibilityDescription { get; set; }

    public static implicit operator JobResponsabilities(JobResposabilitiesModel model) => new()
    {
        ResposabilityDescription = model.ResponsibilityDescription
    };
}