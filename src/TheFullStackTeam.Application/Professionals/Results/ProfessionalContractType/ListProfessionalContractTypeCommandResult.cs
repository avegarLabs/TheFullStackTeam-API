using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Professionals.Results.ProfessionalContractType
{
    public class ListProfessionalContractTypeCommandResult : AppResult<IEnumerable<ProfessionalContractTypeListItem>>
    {
        public ListProfessionalContractTypeCommandResult(IEnumerable<ProfessionalContractTypeListItem> result) : base(result) { }
    }
}
