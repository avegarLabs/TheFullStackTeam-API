using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Jobs.JobResponsability.Command;
using TheFullStackTeam.Application.Jobs.JobResposability.Results;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Jobs.JobResponsability.Handler
{
    public class DeleteJobResponsabilitiesCommandHandler : AppRequestHandler, IRequestHandler<DeleteJobResponsabilitiesCommand, DeleteJobResponsabilityCommandResult>
    {
        public DeleteJobResponsabilitiesCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<DeleteJobResponsabilityCommandResult> Handle(DeleteJobResponsabilitiesCommand request, CancellationToken cancellationToken)
        {
            var jr = await _context.JobResponsabilities.Where(item => item.Id == request.ResponsabilityId).SingleOrDefaultAsync(cancellationToken);
            if (jr != null)
            {
                _context.JobResponsabilities.Remove(jr);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return new DeleteJobResponsabilityCommandResult(true);
        }
    }
}
