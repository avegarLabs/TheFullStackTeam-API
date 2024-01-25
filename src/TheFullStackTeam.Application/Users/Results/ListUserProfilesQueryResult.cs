using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Users.Results
{
    public class ListUserProfilesQueryResult: AppResult<IEnumerable<UserListItem>>
    {
        public ListUserProfilesQueryResult(IEnumerable<UserListItem> list): base(list) { }
    }
}
