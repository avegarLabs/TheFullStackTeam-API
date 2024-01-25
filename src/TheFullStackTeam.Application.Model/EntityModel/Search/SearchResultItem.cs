using TheFullStackTeam.Application.Model.ListItem.Search;

namespace TheFullStackTeam.Application.Model.EntityModel.Search
{

    public class SearchResultItem
    {
        public int TotalItems { get; set; }
        public IList<SearchResultListItem> Results { get; set; }
        public int Page { get; set; }
    }

}