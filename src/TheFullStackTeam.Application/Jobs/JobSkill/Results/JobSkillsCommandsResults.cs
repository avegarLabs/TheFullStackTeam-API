using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Jobs.JobSkill.Results
{
    public class JobSkillsCommandsResults: AppResult<JobSkillListItem>
    {
        public JobSkillsCommandsResults(JobSkillListItem model): base(model) { }
    }
}
