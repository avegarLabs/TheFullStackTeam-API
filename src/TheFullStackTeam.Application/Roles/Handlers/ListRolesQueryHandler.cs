using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Roles.Queries;
using TheFullStackTeam.Application.Roles.Results;
using TheFullStackTeam.Persistence.App;
using WindowsAzure.Table.Extensions;

namespace TheFullStackTeam.Application.Roles.Handlers
{
    public class ListRolesQueryHandler : AppRequestHandler, IRequestHandler<ListRolesQuery, RolesQueryResults>
    {
        public ListRolesQueryHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<RolesQueryResults> Handle(ListRolesQuery request, CancellationToken cancellationToken)
        {
            var results = _context.Roles.AsNoTracking().Select(RolesListItem.Projection).ToList();

            return new RolesQueryResults(results);
                }
    }
}
