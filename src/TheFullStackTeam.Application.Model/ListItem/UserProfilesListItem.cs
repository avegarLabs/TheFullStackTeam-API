namespace TheFullStackTeam.Application.Model.ListItem
{
    public class UserProfilesListItem
    {

        public ProfessionalListItem? Professional { get; set; }
        public List<OrganizationListItem> OrganizationList { get; set; } = new();

    }
}
