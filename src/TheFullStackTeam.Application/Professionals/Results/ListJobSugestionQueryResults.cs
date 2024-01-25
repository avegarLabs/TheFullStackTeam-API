using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Professionals.Results
{
    public class ListJobSugestionQueryResults : AppResult<IEnumerable<JobSugestionsListItem>>
    {
        public ListJobSugestionQueryResults(IEnumerable<JobSugestionsListItem> model) : base(model)
        {
        }
    }
}
