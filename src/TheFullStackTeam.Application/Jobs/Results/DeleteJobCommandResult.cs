namespace TheFullStackTeam.Application.Jobs.Results
{
    public class DeleteJobCommandResult : AppResult<bool>
    {
        public DeleteJobCommandResult(bool success) : base(success) { }
    }
}
