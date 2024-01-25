using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Jobs.Queries;
using TheFullStackTeam.Application.Jobs.Results;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Jobs.Handler
{
    public class ReadJobDetailQueryHandler : AppRequestHandler, IRequestHandler<ReadJobDetailQuery, ReadJobDetailQueryResults>
    {
        public ReadJobDetailQueryHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<ReadJobDetailQueryResults> Handle(ReadJobDetailQuery request, CancellationToken cancellationToken)
        {
            var job = await _context.Jobs
                .Where(j => j.Moniker.Equals(request.JobMoniker))
                .Select(JobDetailListItem.Projection)
                .SingleOrDefaultAsync(cancellationToken);

            return new ReadJobDetailQueryResults(job);
        }
    }
}
