using MediatR;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Professionals.Results.ProfessionalJobType;

namespace TheFullStackTeam.Application.Professionals.Commands.ProfesionalJobType;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class CreateProfessionalJobTypeCommand : IRequest<CreatedProfessionalJobTypeCommandResult>
{
    public Guid ProfessionalId { get; }
    public ProfessionalJobTypeModel Model { get; set; }


    public CreateProfessionalJobTypeCommand(ProfessionalJobTypeModel model, Guid id)
    {
        ProfessionalId = id;
        Model = model;
    }
}

