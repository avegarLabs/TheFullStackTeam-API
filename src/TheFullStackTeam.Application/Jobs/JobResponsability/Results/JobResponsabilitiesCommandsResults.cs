using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Jobs.JobResponsability.Results
{
    public class JobResponsabilitiesCommandsResults: AppResult<JobResponsabilitiesListItem>
    {
        public JobResponsabilitiesCommandsResults(JobResponsabilitiesListItem model): base(model) { }
    }
}
