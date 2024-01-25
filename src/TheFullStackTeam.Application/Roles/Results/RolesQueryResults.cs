using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Roles.Results
{
    public class RolesQueryResults: AppResult<IEnumerable<RolesListItem>>
    {
        public RolesQueryResults(IEnumerable<RolesListItem> list): base(list) { }
    }
}
