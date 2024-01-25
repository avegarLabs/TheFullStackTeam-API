using TheFullStackTeam.Application.Model.ListItem.Search;

namespace TheFullStackTeam.Application.Model.EntityModel.Search
{

    public class SearchResultServicesItem
    {
        public int TotalItems { get; set; }
        public IList<SearchResultServicesListItem> Results { get; set; }
        public int Page { get; set; }
    }

}