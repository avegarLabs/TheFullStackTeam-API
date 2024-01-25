using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFullStackTeam.Application.Users.Results;

namespace TheFullStackTeam.Application.Users.Queries
{
    public class ListUserProfilesQueries:IRequest<ListUserProfilesQueryResult>
    {
    }
}
