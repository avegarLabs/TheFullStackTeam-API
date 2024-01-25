using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Professionals.Queries;
using TheFullStackTeam.Application.Professionals.Results.ProfessionalJobType;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Handlers.ProfesionalJobsType
{
    public class ListProfessionalJobsTypeQueryHandler : IRequestHandler<ListProfessionalJobsTypeQuery, ListProfessionalJobTypeCommandResult>
    {
        private readonly TheFullStackTeamDbContext _context;

        public ListProfessionalJobsTypeQueryHandler(TheFullStackTeamDbContext context)
        {
            _context = context;
        }

        public async Task<ListProfessionalJobTypeCommandResult> Handle(ListProfessionalJobsTypeQuery request, CancellationToken cancellationToken)
        {
            var professionalJobs = await _context.ProfessionalJobTypes.Where(sv => sv.ProfessionalId == request.ProfessionalId).Select(ProfessionalJobTypeListItem.Projection).ToListAsync(cancellationToken);
            return new ListProfessionalJobTypeCommandResult(professionalJobs);
        }
    }
}
