using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Jobs.Queries;
using TheFullStackTeam.Application.Jobs.Results;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Jobs.Handler
{
    public class ListJobsProfessionalQueryHandler : AppRequestHandler, IRequestHandler<ListJobsProfessionalQuery, ListJobsQueryResults>
    {
        public ListJobsProfessionalQueryHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<ListJobsQueryResults> Handle(ListJobsProfessionalQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.Jobs
                .Where(j => j.ProfessionalId == request.ProfessionalId && j.Active)
                .AsNoTracking()
                .Select(JobDetailListItem.Projection)
                .ToListAsync(cancellationToken);

            return new ListJobsQueryResults(list);
        }
    }
}
