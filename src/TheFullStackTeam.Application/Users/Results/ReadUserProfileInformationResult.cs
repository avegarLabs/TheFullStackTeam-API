using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Users.Results
{
    public class ReadUserProfileInformationResult : AppResult<UserListItem>
    {
        public ReadUserProfileInformationResult(UserListItem model) : base(model) { }
    }
}
