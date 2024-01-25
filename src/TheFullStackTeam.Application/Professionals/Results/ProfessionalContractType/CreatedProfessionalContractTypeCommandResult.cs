using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Professionals.Results.ProfessionalContractType
{
    public class CreatedProfessionalContractTypeCommandResult : AppResult<ProfessionalContractTypeListItem>
    {
        public CreatedProfessionalContractTypeCommandResult(ProfessionalContractTypeListItem model) : base(model) { }
    }
}
