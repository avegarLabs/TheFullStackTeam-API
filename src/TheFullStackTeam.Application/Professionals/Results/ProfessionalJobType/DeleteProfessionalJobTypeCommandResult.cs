namespace TheFullStackTeam.Application.Professionals.Results.ProfessionalJobType
{
    public class DeleteProfessionalJobTypeCommandResult : AppResult<bool>
    {
        public DeleteProfessionalJobTypeCommandResult(bool success) : base(success) { }

    }
}
