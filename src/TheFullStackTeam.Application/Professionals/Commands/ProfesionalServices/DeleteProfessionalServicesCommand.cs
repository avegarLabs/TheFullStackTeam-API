using MediatR;
using TheFullStackTeam.Application.Professionals.Results.ProfesionalServicesResults;

namespace TheFullStackTeam.Application.Professionals.Commands.ProfesionalServices;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class DeleteProfessionalServicesCommand : IRequest<DeleteProfessionalServicesCommandResult>
{
    public Guid ProfessionalId { get; set; }
    public Guid ServicesId { get; set; }

    public DeleteProfessionalServicesCommand(Guid pId, Guid id)
    {
        ProfessionalId = pId;
        ServicesId = id;

    }
}

