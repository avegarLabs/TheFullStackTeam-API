using TheFullStackTeam.Application.Model.ListItem.Search;

namespace TheFullStackTeam.Application.Model.EntityModel.Search
{

    public class SearchResultProfilesItem
    {
        public int TotalItems { get; set; }
        public IList<SearchResultProfilesListItem> Results { get; set; }
        public int Page { get; set; }
    }

}