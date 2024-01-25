using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Jobs.Queries;
using TheFullStackTeam.Application.Jobs.Results;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Jobs.Handler
{
    public class ListJobsInUserActiveQueryHandler : AppRequestHandler, IRequestHandler<ListJobsInUserActiveQuery, ListJobsQueryResults>
    {
        public ListJobsInUserActiveQueryHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<ListJobsQueryResults> Handle(ListJobsInUserActiveQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Where(u => u.AccountId.Equals(request.AccountId))
                .AsNoTracking()
                .SingleOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.AccountId);
            }

            var result = await _context.Jobs
                .Include(j => j.Professional)
                .Include(j => j.Organization)
                .Where(j => (j.Professional.UserId == user.Id) || (j.Organization.UserId == user.Id))
                .Select(JobDetailListItem.Projection)
                .ToListAsync(cancellationToken);

            return new ListJobsQueryResults(result);
        }
    }
}
