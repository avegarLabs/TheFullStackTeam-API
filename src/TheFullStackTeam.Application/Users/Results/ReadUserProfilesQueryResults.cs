using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Users.Results
{
    public class ReadUserProfilesQueryResults: AppResult<UserProfilesListItem>
    {
        public ReadUserProfilesQueryResults(UserProfilesListItem model): base(model) { }
    }
}
