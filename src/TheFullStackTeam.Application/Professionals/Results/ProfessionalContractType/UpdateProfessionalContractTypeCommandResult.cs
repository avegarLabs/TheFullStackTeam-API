using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Professionals.Results.ProfessionalContractType
{
    public class UpdateProfessionalContractTypeCommandResult : AppResult<ProfessionalContractTypeListItem>
    {
        public UpdateProfessionalContractTypeCommandResult(ProfessionalContractTypeListItem model) : base(model) { }
    }
}
