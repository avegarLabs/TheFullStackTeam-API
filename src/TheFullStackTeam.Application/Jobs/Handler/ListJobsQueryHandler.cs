using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Jobs.Queries;
using TheFullStackTeam.Application.Jobs.Results;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Jobs.Handler
{
    public class ListJobsQueryHandler : AppRequestHandler, IRequestHandler<ListJobsQuery, ListJobsQueryResults>
    {
        public ListJobsQueryHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<ListJobsQueryResults> Handle(ListJobsQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Jobs.AsNoTracking().Select(JobDetailListItem.Projection).ToListAsync(cancellationToken);    
            return new ListJobsQueryResults(result);
        }
    }
}
