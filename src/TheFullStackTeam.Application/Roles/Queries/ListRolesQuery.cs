using MediatR;
using TheFullStackTeam.Application.Roles.Results;

namespace TheFullStackTeam.Application.Roles.Queries
{
    public class ListRolesQuery: IRequest<RolesQueryResults>
    {
    }
}
