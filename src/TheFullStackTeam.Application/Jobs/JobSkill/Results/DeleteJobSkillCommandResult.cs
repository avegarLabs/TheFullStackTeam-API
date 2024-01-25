namespace TheFullStackTeam.Application.Jobs.JobSkill.Results
{
    public class DeleteJobSkillCommandResult : AppResult<bool>
    {
        public DeleteJobSkillCommandResult(bool success) : base(success) { }
    }
}
