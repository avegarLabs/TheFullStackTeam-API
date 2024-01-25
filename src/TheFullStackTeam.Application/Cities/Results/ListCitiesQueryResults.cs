using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Cities.Results
{
    public class ListCitiesQueryResults : AppResult<IEnumerable<CityListItem>>
    {
        public ListCitiesQueryResults(IEnumerable<CityListItem> model) : base(model)
        {
        }
    }
}
