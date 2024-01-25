using MediatR;
using TheFullStackTeam.Application.Cities.Results;

namespace TheFullStackTeam.Application.Cities.Queries
{
    public class ListCitiesByCountryQuery:IRequest<ListCitiesQueryResults>
    {
        public Guid CountryId { get; set; }

        public ListCitiesByCountryQuery(Guid id)
        {
            CountryId = id; 
        }    
    }
}
