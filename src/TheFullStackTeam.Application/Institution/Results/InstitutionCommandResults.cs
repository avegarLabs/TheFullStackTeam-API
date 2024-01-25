using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Institution.Results
{
    public class InstitutionCommandResults : AppResult<InstitutionListItem>
    {
        public InstitutionCommandResults(InstitutionListItem model) : base(model)
        {
        }
    }
}
