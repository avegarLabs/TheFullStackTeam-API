using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Languages.Queries;
using TheFullStackTeam.Application.Languages.Results;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Persistence.App;
using WindowsAzure.Table.Extensions;

namespace TheFullStackTeam.Application.Languages.Handlers
{
    public class ListLanguageQueryHandler : AppRequestHandler, IRequestHandler<ListLanguageQuery, LanguageQueriesResults>
    {
        public ListLanguageQueryHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<LanguageQueriesResults> Handle(ListLanguageQuery request, CancellationToken cancellationToken)
        {
            var results = _context.Languages.AsNoTracking().Select(LanguageListItem.Projection).ToList();

            return new LanguageQueriesResults(results);
        }
    }
}
