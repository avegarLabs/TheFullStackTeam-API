using MediatR;
using TheFullStackTeam.Application.Users.Results;

namespace TheFullStackTeam.Application.Users.Queries
{
    public class ReadUserProfilesQuery : IRequest<ReadUserProfilesQueryResults>
    {
        public string AccountId { get; set; }
        public ReadUserProfilesQuery(string id)
        {
            AccountId = id;
        }
    }
}
