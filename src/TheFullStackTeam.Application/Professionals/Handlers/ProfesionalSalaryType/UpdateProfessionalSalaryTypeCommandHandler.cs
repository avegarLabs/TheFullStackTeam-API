using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Professionals.Commands.ProfesionalServices;
using TheFullStackTeam.Application.Professionals.Results.ProfessionalSalaryType;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Handlers.ProfesionalSalaryType
{
    public class UpdateProfessionalSalaryTypeCommandHandler : AppRequestHandler, IRequestHandler<UpdateProfessionalSalaryTypeCommand, UpdateProfessionalSalaryTypeCommandResult>
    {
        public UpdateProfessionalSalaryTypeCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<UpdateProfessionalSalaryTypeCommandResult> Handle(UpdateProfessionalSalaryTypeCommand request, CancellationToken cancellationToken)
        {
            var professionalSalary = await _context.ProfessionalSalaryTypes.AsNoTracking().Where(ps => ps.Id == request.SalaryTypeId && ps.ProfessionalId == request.ProfessionalId).SingleOrDefaultAsync(cancellationToken);
            if (professionalSalary == null)
            {
                throw new NotFoundException(nameof(ProfessionalSalaryType), request.SalaryTypeId);
            }

            professionalSalary.PaymentPeriod = request.Model.PaymentPeriod;
            professionalSalary.Amount = request.Model.Amount;
            professionalSalary.Currency = request.Model.Currency;
            _context.ProfessionalSalaryTypes.Update(professionalSalary);
            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateProfessionalSalaryTypeCommandResult(professionalSalary);

        }
    }
}
