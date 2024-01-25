using MediatR;
using TheFullStackTeam.Application.Professionals.Results.ProfessionalJobType;

namespace TheFullStackTeam.Application.Professionals.Commands.ProfesionalJobType;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class DeleteProfessionalJobTypeCommand : IRequest<DeleteProfessionalJobTypeCommandResult>
{
    public Guid ProfessionalJobTypeId { get; set; }

    public DeleteProfessionalJobTypeCommand(Guid id)
    {
        ProfessionalJobTypeId = id;
    }
}

