using MediatR;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Professionals.Results.ProfesionalServicesResults;

namespace TheFullStackTeam.Application.Professionals.Commands.ProfesionalServices;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class CreateProfessionalServicesTypeCommand : IRequest<CreatedProfessionalServicesCommandResult>
{
    public Guid ProfessionalId { get; }
    public ProfessionalServiceModel Model { get; set; }


    public CreateProfessionalServicesTypeCommand(ProfessionalServiceModel model, Guid id)
    {
        ProfessionalId = id;
        Model = model;
    }
}

