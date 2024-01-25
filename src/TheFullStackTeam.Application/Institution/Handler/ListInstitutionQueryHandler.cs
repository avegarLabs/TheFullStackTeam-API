using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Institution.Queries;
using TheFullStackTeam.Application.Institution.Results;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Institution.Handler
{
    public class ListInstitutionQueryHandler : AppRequestHandler, IRequestHandler<ListInstitutionQuery, InstitutionListQueryResults>
    {
        public ListInstitutionQueryHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<InstitutionListQueryResults> Handle(ListInstitutionQuery request, CancellationToken cancellationToken)
        {
            var results = await _context.institutions.AsNoTracking().Select(InstitutionListItem.Projection).ToListAsync(cancellationToken);
            return new InstitutionListQueryResults(results);


        }
    }
}
