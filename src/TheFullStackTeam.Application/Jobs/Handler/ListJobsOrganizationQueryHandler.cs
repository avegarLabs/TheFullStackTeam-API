using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFullStackTeam.Application.Jobs.Queries;
using TheFullStackTeam.Application.Jobs.Results;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Jobs.Handler
{
    public class ListJobsOrganizationQueryHandler : AppRequestHandler, IRequestHandler<ListJobsOrganizationQuery, ListJobsQueryResults>
    {
        public ListJobsOrganizationQueryHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<ListJobsQueryResults> Handle(ListJobsOrganizationQuery request, CancellationToken cancellationToken)
        {
            var results = await _context.Jobs
                .Where(j => j.OrganizationId == request.OrganizationId && j.Active)
                .AsNoTracking()
                .Select(JobDetailListItem.Projection)
                .ToListAsync(cancellationToken);

            return new ListJobsQueryResults(results);
        }
    }
}
