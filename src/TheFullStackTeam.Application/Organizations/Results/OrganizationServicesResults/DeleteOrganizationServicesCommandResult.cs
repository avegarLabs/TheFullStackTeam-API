namespace TheFullStackTeam.Application.Organizations.Results.OrganizationServicesResults
{
    public class DeleteOrganizationServicesCommandResult : AppResult<bool>
    {
        public DeleteOrganizationServicesCommandResult(bool success) : base(success) { }
    }
}
