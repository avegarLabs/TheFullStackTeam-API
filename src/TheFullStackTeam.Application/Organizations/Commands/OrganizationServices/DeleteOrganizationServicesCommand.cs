using MediatR;
using TheFullStackTeam.Application.Organizations.Results.OrganizationServicesResults;
using TheFullStackTeam.Application.Professionals.Results.ProfesionalServicesResults;

namespace TheFullStackTeam.Application.Organizations.Commands.OrganizationsServices;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class DeleteOrganizationServicesCommand : IRequest<DeleteOrganizationServicesCommandResult>
{
    public Guid OrganizationId { get; set; }
    public Guid ServicesId { get; set; }

    public DeleteOrganizationServicesCommand(Guid pId, Guid id)
    {
        OrganizationId = pId;
        ServicesId = id;

    }
}

