namespace TheFullStackTeam.Application.Professionals.Results.ProfessionalSalaryType
{
    public class DeleteProfessionalSalaryTypeCommandResult : AppResult<bool>
    {
        public DeleteProfessionalSalaryTypeCommandResult(bool success) : base(success) { }

    }
}
