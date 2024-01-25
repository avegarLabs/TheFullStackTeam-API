using MediatR;
using TheFullStackTeam.Application.Professionals.Results.ProfessionalSalaryType;

namespace TheFullStackTeam.Application.Professionals.Commands.ProfesionalServices;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class DeleteProfessionalSalaryTypeCommand : IRequest<DeleteProfessionalSalaryTypeCommandResult>
{
    public Guid SalaryTypeId { get; set; }

    public DeleteProfessionalSalaryTypeCommand(Guid id)
    {
        SalaryTypeId = id;

    }
}

