namespace TheFullStackTeam.Application.Professionals.Results.ProfesionalServicesResults
{
    public class DeleteProfessionalServicesCommandResult : AppResult<bool>
    {
        public DeleteProfessionalServicesCommandResult(bool success) : base(success) { }
    }
}
