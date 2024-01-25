using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Organizations.Results.OrganizationServicesResults
{
    public class ListOrganizationServicesQueryResult : AppResult<IEnumerable<OrganizationServiceListItem>>
    {
        public ListOrganizationServicesQueryResult(IEnumerable<OrganizationServiceListItem> model) : base(model) { }
    }
}
