using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Organizations.Results.OrganizationsServicesResults
{
    public class CreatedOrganizationServicesCommandResult : AppResult<OrganizationServiceListItem>
    {
        public CreatedOrganizationServicesCommandResult(OrganizationServiceListItem model) : base(model) { }

    }
}
