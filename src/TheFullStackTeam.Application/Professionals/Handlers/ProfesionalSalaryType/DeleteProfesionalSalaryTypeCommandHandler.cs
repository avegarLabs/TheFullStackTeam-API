using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Professionals.Commands.ProfesionalServices;
using TheFullStackTeam.Application.Professionals.Results.ProfessionalSalaryType;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Handlers.ProfesionalSalaryType
{
    public class DeleteProfesionalSalaryTypeCommandHandler : AppRequestHandler, IRequestHandler<DeleteProfessionalSalaryTypeCommand, DeleteProfessionalSalaryTypeCommandResult>
    {
        public DeleteProfesionalSalaryTypeCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<DeleteProfessionalSalaryTypeCommandResult> Handle(DeleteProfessionalSalaryTypeCommand request, CancellationToken cancellationToken)
        {
            var profesionalSalary = await _context.ProfessionalSalaryTypes.AsNoTracking()
                .Where(ps => ps.Id == request.SalaryTypeId)
                .SingleOrDefaultAsync(cancellationToken);

            if (profesionalSalary == null)
            {
                throw new NotFoundException(nameof(ProfessionalSalaryType), request.SalaryTypeId);
            }

            _context.ProfessionalSalaryTypes.Remove(profesionalSalary);
            await _context.SaveChangesAsync(cancellationToken);
            return new DeleteProfessionalSalaryTypeCommandResult(true);

        }
    }
}

