using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Jobs.Results
{
    public class CreateJobCommandResult : AppResult<JobListItem>
    {
        public CreateJobCommandResult(JobListItem job) : base(job) { }
    }
}
