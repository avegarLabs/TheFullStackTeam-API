using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Professionals.Commands.ProfesionalJobType;
using TheFullStackTeam.Application.Professionals.Results.ProfessionalJobType;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Handlers.ProfesionalJobType
{
    public class DeleteProfesionalJobTypeCommandHandler : AppRequestHandler, IRequestHandler<DeleteProfessionalJobTypeCommand, DeleteProfessionalJobTypeCommandResult>
    {
        public DeleteProfesionalJobTypeCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<DeleteProfessionalJobTypeCommandResult> Handle(DeleteProfessionalJobTypeCommand request, CancellationToken cancellationToken)
        {
            var profesionalJob = await _context.ProfessionalJobTypes.AsNoTracking()
              .Where(pc => pc.Id == request.ProfessionalJobTypeId)
              .SingleOrDefaultAsync(cancellationToken);
            if (profesionalJob == null)
            {
                throw new NotFoundException(nameof(ProfesionalJobsType), request.ProfessionalJobTypeId);
            }
            _context.ProfessionalJobTypes.Remove(profesionalJob);
            await _context.SaveChangesAsync(cancellationToken);
            return new DeleteProfessionalJobTypeCommandResult(true);
        }

    }
}

