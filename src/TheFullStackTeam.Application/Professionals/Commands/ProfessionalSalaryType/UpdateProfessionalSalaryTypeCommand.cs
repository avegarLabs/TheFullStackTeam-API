using MediatR;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Professionals.Results.ProfessionalSalaryType;

namespace TheFullStackTeam.Application.Professionals.Commands.ProfesionalServices;

/// <inheritdoc cref="IRequest{TResponse}"/>
public class UpdateProfessionalSalaryTypeCommand : IRequest<UpdateProfessionalSalaryTypeCommandResult>
{
    public Guid ProfessionalId { get; }
    public Guid SalaryTypeId { get; }
    public ProfessionalSalaryTypeModel Model { get; set; }

    public UpdateProfessionalSalaryTypeCommand(ProfessionalSalaryTypeModel model, Guid pId, Guid stId)
    {
        ProfessionalId = pId;
        SalaryTypeId = stId;
        Model = model;
    }
}
