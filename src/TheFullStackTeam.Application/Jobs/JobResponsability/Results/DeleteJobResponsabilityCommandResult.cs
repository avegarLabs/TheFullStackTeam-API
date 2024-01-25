namespace TheFullStackTeam.Application.Jobs.JobResposability.Results
{
    public class DeleteJobResponsabilityCommandResult : AppResult<bool>
    {
        public DeleteJobResponsabilityCommandResult(bool success) : base(success) { }
    }
}
