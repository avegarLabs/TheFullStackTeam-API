using MediatR;
using TheFullStackTeam.Application.Professionals.Results.ProfessionalContractType;

namespace TheFullStackTeam.Application.Professionals.Commands.ProfesionalContractType;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class DeleteProfessionalContractTypeCommand : IRequest<DeleteProfessionalContractTypeCommandResult>
{
    public Guid ContratTypeId { get; set; }

    public DeleteProfessionalContractTypeCommand(Guid id)
    {
        ContratTypeId = id;

    }
}

