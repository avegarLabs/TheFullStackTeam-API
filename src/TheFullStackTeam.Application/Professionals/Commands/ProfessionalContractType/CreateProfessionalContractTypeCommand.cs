using MediatR;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Professionals.Results.ProfessionalContractType;

namespace TheFullStackTeam.Application.Professionals.Commands.ProfesionalContractType;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class CreateProfessionalContractTypeCommand : IRequest<CreatedProfessionalContractTypeCommandResult>
{
    public Guid ProfessionalId { get; }
    public ProfessionalContractTypeModel Model { get; set; }


    public CreateProfessionalContractTypeCommand(ProfessionalContractTypeModel model, Guid id)
    {
        ProfessionalId = id;
        Model = model;
    }
}

