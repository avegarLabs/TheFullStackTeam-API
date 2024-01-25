using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Organizations.Results
{
    public class ReadOrganizationDetailsResults: AppResult<OrganizationListItem>
    {
        public ReadOrganizationDetailsResults(OrganizationListItem model): base(model) { }
    }
}
