using MediatR;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Organizations.Results.OrganizationsServicesResults;

namespace TheFullStackTeam.Application.Organizations.Commands.OrganizationServices;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class UpdateOrganizationServicesCommand : IRequest<UpdateOrganizationServicesCommandResult>
{
    public Guid OrganizationId { get; set; }
    public Guid ServiceId { get; set; }
    public OrganizationServiceModel Model { get; set; }

    public UpdateOrganizationServicesCommand(OrganizationServiceModel model, Guid pId, Guid sId)
    {
        OrganizationId = pId;
        ServiceId = sId;
        Model = model;
    }
}
