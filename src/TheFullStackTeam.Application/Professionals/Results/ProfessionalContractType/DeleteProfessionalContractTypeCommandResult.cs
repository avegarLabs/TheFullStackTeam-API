namespace TheFullStackTeam.Application.Professionals.Results.ProfessionalContractType
{
    public class DeleteProfessionalContractTypeCommandResult : AppResult<bool>
    {
        public DeleteProfessionalContractTypeCommandResult(bool success) : base(success) { }

    }
}
