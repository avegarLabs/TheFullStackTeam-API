using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Organizations.Results.OrganizationsServicesResults
{
    public class UpdateOrganizationServicesCommandResult : AppResult<OrganizationServiceListItem>
    {
        public UpdateOrganizationServicesCommandResult(OrganizationServiceListItem model) : base(model) { }
    }
}
