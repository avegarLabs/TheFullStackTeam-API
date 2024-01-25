using MediatR;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Organizations.Results.OrganizationsServicesResults;

namespace TheFullStackTeam.Application.Organizations.Commands.OrganizationServices;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class CreateOrganizationServicesTypeCommand : IRequest<CreatedOrganizationServicesCommandResult>
{
    public Guid OrganizationId { get; }
    public OrganizationServiceModel Model { get; set; }


    public CreateOrganizationServicesTypeCommand(OrganizationServiceModel model, Guid id)
    {
        OrganizationId = id;
        Model = model;
    }
}

