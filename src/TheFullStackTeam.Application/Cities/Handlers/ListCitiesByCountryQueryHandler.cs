using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Cities.Queries;
using TheFullStackTeam.Application.Cities.Results;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Persistence.App;
using WindowsAzure.Table.Extensions;

namespace TheFullStackTeam.Application.Cities.Handlers
{
    public class ListCitiesByCountryQueryHandler : AppRequestHandler, IRequestHandler<ListCitiesByCountryQuery, ListCitiesQueryResults>
    {
        public ListCitiesByCountryQueryHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<ListCitiesQueryResults> Handle(ListCitiesByCountryQuery request, CancellationToken cancellationToken)
        {
           var results =  _context.Cities.AsNoTracking().Where(c => c.CountryId.Equals(request.CountryId)).Select(CityListItem.Projection).ToList();
           return new ListCitiesQueryResults(results);
        }
    }
}
