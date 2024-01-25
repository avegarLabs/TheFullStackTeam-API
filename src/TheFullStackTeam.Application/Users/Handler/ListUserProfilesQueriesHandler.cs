using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Users.Queries;
using TheFullStackTeam.Application.Users.Results;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Users.Handler
{
    public class ListUserProfilesQueriesHandler : AppRequestHandler, IRequestHandler<ListUserProfilesQueries, ListUserProfilesQueryResult>
    {
        public ListUserProfilesQueriesHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<ListUserProfilesQueryResult> Handle(ListUserProfilesQueries request, CancellationToken cancellationToken)
        {
           var results = await _context.Users.AsNoTracking().Select(UserListItem.Projection).ToListAsync(cancellationToken);
            return new ListUserProfilesQueryResult(results);
        }
    }
}
