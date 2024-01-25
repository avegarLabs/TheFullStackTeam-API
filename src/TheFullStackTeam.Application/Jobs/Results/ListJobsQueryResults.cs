using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Jobs.Results
{
    public class ListJobsQueryResults : AppResult<IEnumerable<JobDetailListItem>>
    {
        public ListJobsQueryResults(IEnumerable<JobDetailListItem> model) : base(model) { }
    }
}
