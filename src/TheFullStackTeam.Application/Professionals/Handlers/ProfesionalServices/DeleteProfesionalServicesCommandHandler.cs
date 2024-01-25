using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Professionals.Commands.ProfesionalServices;
using TheFullStackTeam.Application.Professionals.Results.ProfesionalServicesResults;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Handlers.ProfesionalServices
{
    public class DeleteProfesionalServicesCommandHandler : AppRequestHandler, IRequestHandler<DeleteProfessionalServicesCommand, DeleteProfessionalServicesCommandResult>
    {
        public DeleteProfesionalServicesCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<DeleteProfessionalServicesCommandResult> Handle(DeleteProfessionalServicesCommand request, CancellationToken cancellationToken)
        {
            var profesionalService = await _context.ProfessionalSevices.AsNoTracking()
            .Where(ps => ps.Id == request.ServicesId && ps.ProfessionalId == request.ProfessionalId)
            .SingleOrDefaultAsync(cancellationToken);

            if (profesionalService == null)
            {
                throw new NotFoundException(nameof(ProfessionalSevices), request.ServicesId);
            }
             _context.ProfessionalSevices.Remove(profesionalService);
            await _context.SaveChangesAsync(cancellationToken);
            return new DeleteProfessionalServicesCommandResult(true);
        }
    }
}
