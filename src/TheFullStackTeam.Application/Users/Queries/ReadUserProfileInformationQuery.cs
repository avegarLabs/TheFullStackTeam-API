using MediatR;
using TheFullStackTeam.Application.Users.Results;

namespace TheFullStackTeam.Application.Users.Queries
{
    public class ReadUserProfileInformationQuery : IRequest<ReadUserProfileInformationResult>
    {
        public string AccountId { get; set; }
        public ReadUserProfileInformationQuery(string id)
        {
            AccountId = id;
        }
    }
}
