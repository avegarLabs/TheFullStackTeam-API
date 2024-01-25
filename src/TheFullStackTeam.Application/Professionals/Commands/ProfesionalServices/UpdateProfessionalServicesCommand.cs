using MediatR;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Professionals.Results.ProfesionalServicesResults;

namespace TheFullStackTeam.Application.Professionals.Commands.ProfesionalServices;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class UpdateProfessionalServicesCommand : IRequest<UpdateProfessionalServicesCommandResult>
{
    public Guid ProfessionalId { get; set; }
    public Guid ServiceId { get; set; }
    public ProfessionalServiceModel Model { get; set; }

    public UpdateProfessionalServicesCommand(ProfessionalServiceModel model, Guid pId, Guid sId)
    {
        ProfessionalId = pId;
        ServiceId = sId;
        Model = model;
    }
}
