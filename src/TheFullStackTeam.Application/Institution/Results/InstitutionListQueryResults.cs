using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Institution.Results
{
    public class InstitutionListQueryResults : AppResult<IEnumerable<InstitutionListItem>>
    {
        public InstitutionListQueryResults(IEnumerable<InstitutionListItem> model) : base(model)
        {
        }
    }
}
