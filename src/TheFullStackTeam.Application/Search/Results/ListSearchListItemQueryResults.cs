using TheFullStackTeam.Application.Model.ListItem.Search;

namespace TheFullStackTeam.Application.Search.Results
{
    public class ListSearchListItemQueryResults: AppResult<IEnumerable<SearchResultListItem>>
    {
        public ListSearchListItemQueryResults(IEnumerable<SearchResultListItem> results) : base(results) { } 
    }
}
