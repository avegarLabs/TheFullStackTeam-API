using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Jobs.Results
{
    public class ReadJobDetailQueryResults : AppResult<JobDetailListItem>
    {
        public ReadJobDetailQueryResults(JobDetailListItem model) : base(model) { }
    }
}
