using MediatR;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Professionals.Results.ProfessionalSalaryType;

namespace TheFullStackTeam.Application.Professionals.Commands.ProfesionalSalaryType;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class CreateProfessionalSalaryTypeCommand : IRequest<CreatedProfessionalSalaryTypeCommandResult>
{
    public Guid ProfessionalId { get; }
    public ProfessionalSalaryTypeModel Model { get; set; }


    public CreateProfessionalSalaryTypeCommand(ProfessionalSalaryTypeModel model, Guid id)
    {
        ProfessionalId = id;
        Model = model;
    }
}

